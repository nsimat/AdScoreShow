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

            if(fileExt == ".CSV")
            {
                string fileName = Path.GetFileName(viewModel.UpLoadedCsvFile.FileName);
                string path = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + fileName;
                viewModel.UpLoadedCsvFile.SaveAs(path);

                using (StreamReader input = System.IO.File.OpenText(path))
                using (CsvReader csvReader = new CsvReader(input, CultureInfo.InvariantCulture))
                {
                    IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();

                    csvReader.Configuration.Delimiter = ";";

                    //Processing records to seed the database
                    SeedDatabase(records);
                    var segments = new HashSet<Segment>();
                    var brands = new HashSet<Market>();
                    var markets = new HashSet<Market>();

                    


                }


            }
            else
            {

            }

            

            
            return View();
        }

        private void SeedDatabase(IEnumerable<dynamic> records)
        {
            using(var dbContext = new AdScoreShowDbContext())
            {
                foreach (var record in records)
                {
                    var segment = new Segment { Category = record.Segment };
                    dbContext.Segments.Add(segment);
                    //dbContext.SaveChanges();

                    var brand = new Brand { BrandName = record.Brand };
                    var market = new Market { Country = record.Market };
                    var advertisement = new Advertisement
                    {
                        Copy_Name = record.Copy_Name,
                        Copy_Duration = record.Copy_Duration,//Convert to int
                        SegmentID = 0,
                        BrandID = 0
                    };
                    var advertAired = new AdvertAired
                    {
                        AdvertisementID = 0,
                        MarketID = 0,
                        Year = record.Year,//convert string to int
                        Score_1 = record.Score_1,
                        Score_2 = record.Score_2
                    };
                }
            }
        }
    }
}