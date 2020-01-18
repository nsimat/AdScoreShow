using AdScoreShow.Models;
using System;
using System.Collections.Generic;
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


            return View();
        }
    }
}