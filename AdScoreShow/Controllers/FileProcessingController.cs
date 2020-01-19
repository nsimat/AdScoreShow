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


                }


            }
            else
            {

            }

            

            
            return View();
        }
    }
}