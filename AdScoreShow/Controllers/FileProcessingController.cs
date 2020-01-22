using AdScoreShow.Models;
using AdScoreShow.Repository;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdScoreShow.Controllers
{
    public class FileProcessingController : Controller
    {
        // GET: FileProcessing
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewFileProcessing()
        {
            var uploadedFile = new UploadCsvFileViewModel();

            return View(uploadedFile);
        }

        [HttpPost]
        public ActionResult NewFileProcessing(UploadCsvFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string fileExt = Path.GetExtension(viewModel.UpLoadedCsvFile.FileName).ToUpper();

            if (fileExt == ".CSV")
            {
                string fileName = Path.GetFileName(viewModel.UpLoadedCsvFile.FileName);
                string path = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + fileName;
                viewModel.UpLoadedCsvFile.SaveAs(path);

                using (StreamReader input = System.IO.File.OpenText(path))
                using (CsvReader csvReader = new CsvReader(input, CultureInfo.InvariantCulture))
                {
                    IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();

                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;

                    //Processing records to seed the database
                    SeedDatabase(records);
                }
                return RedirectToAction("Index", "DataShow");

            }
            else
            {
                ViewData["error"] = "upload failed";
                return View(viewModel);
            }            
        }

        private void SeedDatabase(IEnumerable<dynamic> records)
        {

            foreach (var record in records)
            {
                //Insert segment and market objects in the database
                string brandName = record.Brand;
                var segment = new Segment { Category = record.Segment };
                var market = new Market { Country = record.Market };

                using (var dbContext = new AdScoreShowDbContext())
                {
                    dbContext.Segments.Add(segment);
                    dbContext.Markets.Add(market);
                    dbContext.SaveChanges();
                }

                //Insert a brand object in the database
                using (var dbContext = new AdScoreShowDbContext())
                {
                    var segmt = dbContext.Segments.Single(s => s.Category == segment.Category);
                    var brand = new Brand
                    {
                        Name = brandName,
                        SegmentID = segmt.Id
                    };
                    dbContext.Brands.Add(brand);
                    dbContext.SaveChanges();
                }

                //Insert an Advertisement object in the database
                using (var dbContext = new AdScoreShowDbContext())
                {
                    var segmt = dbContext.Segments.Single(s => s.Category == segment.Category);

                    var brd = dbContext.Brands.Single(b => b.Name == brandName);

                    var advertisement = new Advertisement
                    {
                        Copy_Name = record.Copy_Name,
                        Copy_Duration = Int32.Parse(record.Copy_Duration),
                        SegmentID = segmt.Id,
                        BrandID = brd.Id
                    };
                    dbContext.Advertisements.Add(advertisement);
                    dbContext.SaveChanges();
                }                               
                
                //Finally, insert an AdvertAired object in the database
                using(var dbContext = new AdScoreShowDbContext())
                {
                    var sgmt = dbContext.Segments.Single(s => s.Category == segment.Category);
                    var mkt = dbContext.Markets.Single(m => m.Country == market.Country);
                    string score1 = record.Score_1;
                    string score2 = record.Score_2;
                    string year = record.Year;

                    var advertAired = new AdvertAired
                    {
                        AdvertisementID = sgmt.Id,
                        MarketID = mkt.Id,
                        Year = string.IsNullOrEmpty(year)  || year.Equals("N/A") ? null : (int?)Int32.Parse(year),
                        Score_1 = string.IsNullOrEmpty(score1) || score1.Equals("N/A") ? null : (int?)Int32.Parse(record.Score_1),
                        Score_2 = string.IsNullOrEmpty(score2) || score2.Equals("N/A") ? null : (int?)Int32.Parse(score2.Substring(0,2))
                    };
                    dbContext.AdvertAireds.Add(advertAired);
                    dbContext.SaveChanges();
                }                
            }
        }
    }
}