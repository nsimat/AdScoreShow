using AdScoreShow.Models;
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
            var uploadedFile = new AdvertisementViewModel();

            return View(uploadedFile);
        }

        [HttpPost]
        public ActionResult NewFileProcessing(AdvertisementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string fileExt = Path.GetExtension(viewModel.UpLoadedCsvFile.FileName).ToUpper();

            if(fileExt == ".CSV")
            {
                string inputFilePath = viewModel.UpLoadedCsvFile.FileName;

                using (StreamReader input = System.IO.File.OpenText(inputFilePath))
                using (CsvReader csvReader = new CsvReader(input, CultureInfo.InvariantCulture))
                {
                    IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();


                }


            }
            else
            {

            }

            

            
            return View();
        }
    }
}