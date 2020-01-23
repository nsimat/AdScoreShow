using AdScoreShow.Models;
using AdScoreShow.Repository;
using AdScoreShow.Utility;
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
                    IEnumerable<CsvRecord> records = csvReader.GetRecords<CsvRecord>();

                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.IgnoreBlankLines = true;
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.RegisterClassMap<CsvRecordMap>();

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

        private void SeedDatabase(IEnumerable<CsvRecord> records)
        {

            foreach (CsvRecord record in records)
            {
                //check if the value of the segment is already present in the database or not
                //if not, insert it in the database
                if (!ValueAlreadyPresent(record, 's'))
                {
                    using(var dbContext = new AdScoreShowDbContext())
                    {
                        var segment = new Segment { Category = record.Segment };
                        dbContext.Segments.Add(segment);
                        dbContext.SaveChanges();
                    }
                }

                //check if the value of the market is already present in the database or not
                //if not, insert it in the database
                if((!ValueAlreadyPresent(record, 'm')))
                {
                    using (var dbContext = new AdScoreShowDbContext())
                    {
                        var market = new Market { Country = record.Market };
                        dbContext.Markets.Add(market);
                        dbContext.SaveChanges();
                    }
                }                

                //check if the value of the brand is present in the database or not
                //if not, insert it in the database
                if(!ValueAlreadyPresent(record, 'b'))
                {
                    using (var dbContext = new AdScoreShowDbContext())
                    {
                        var segment = dbContext.Segments.Single(s => s.Category == record.Segment);
                        var brand = new Brand
                        {
                            Name = record.Brand,
                            SegmentID = segment.Id
                        };
                        dbContext.Brands.Add(brand);
                        dbContext.SaveChanges();
                    }
                }                

                //check if the value of the couple (Copy_Name, Copy_Duration) is present in the database or not
                //if not, insert a new advertisement in the database
                if(!ValueAlreadyPresent(record, 'a'))
                {
                    using (var dbContext = new AdScoreShowDbContext())
                    {
                        var segment = dbContext.Segments.Single(s => s.Category == record.Segment);
                        var brand = dbContext.Brands.Single(b => b.Name == record.Brand);

                        var advertisement = new Advertisement
                        {
                            Copy_Name = record.Copy_Name,
                            Copy_Duration = record.Copy_Duration,
                            SegmentID = segment.Id,
                            BrandID = brand.Id
                        };
                        dbContext.Advertisements.Add(advertisement);
                        dbContext.SaveChanges();
                    }
                }                

                //check if the couple (AdvertisementID, MarketID) is present in the database or not
                //if not, finally, insert it in the database
                if(!ValueAlreadyPresent(record, 'n')){
                    using (var dbContext = new AdScoreShowDbContext())
                    {
                        var advertisement = dbContext.Advertisements.Single(a => a.Copy_Name == record.Copy_Name & a.Copy_Duration == record.Copy_Duration);
                        var market = dbContext.Markets.Single(m => m.Country == record.Market);

                        var advertAired = new AdvertAired
                        {
                            AdvertisementID = advertisement.Id,
                            MarketID = market.Id,
                            Year = record.Year,
                            Score_1 = record.Score_1,
                            Score_2 = record.Score_2
                        };
                        dbContext.AdvertAireds.Add(advertAired);
                        dbContext.SaveChanges();
                    }
                }          
                
            }
        }

        private bool ValueAlreadyPresent(CsvRecord record, char c)
        {
            bool result = false;

            using (var dbContext = new AdScoreShowDbContext())
            {
                switch (c)
                {
                    case 's':                        
                        Segment segment = dbContext.Segments.SingleOrDefault(s => s.Category == record.Segment);
                        if (segment != null)
                            result = true;
                        break;
                    case 'b':
                        string bd = record.Brand;
                        Brand brand = dbContext.Brands.SingleOrDefault(b => b.Name == record.Brand);
                        if (brand != null)
                            result = true;
                        break;
                    case 'm':
                        Market market = dbContext.Markets.SingleOrDefault(m => m.Country == record.Market);
                        if (market != null)
                            result = true;
                        break;
                    case 'a':
                        Advertisement advert = dbContext.Advertisements
                                                        .SingleOrDefault(a => a.Copy_Name == record.Copy_Name 
                                                                && a.Copy_Duration == record.Copy_Duration);
                        if (advert != null)
                            result = true;
                        break;
                    case 'n':
                        Advertisement ads = dbContext.Advertisements
                                                     .SingleOrDefault(a => a.Copy_Name == record.Copy_Name 
                                                                      & a.Copy_Duration == record.Copy_Duration);
                        Market mkt = dbContext.Markets.SingleOrDefault(m => m.Country == record.Market);
                        AdvertAired adv = dbContext.AdvertAireds.SingleOrDefault(a => a.AdvertisementID == ads.Id && a.MarketID == mkt.Id);

                        if (adv != null)
                            result = true;
                        break;
                    default:
                        break;
                }
            }            
            return result;
        }
    }
}