using AdScoreShow.Models;
using AdScoreShow.Repository;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdScoreShow.Controllers
{
    public class DataShowController : Controller
    {
        // GET: DataShow
        public ActionResult Index()
        {
            using(var dbContext = new AdScoreShowDbContext())
            {
                AdvertViewModel viewModel = new AdvertViewModel();
                var datas = dbContext.Advertisements.Include(b => b.Brand).Include(s => s.Segment).ToList();
            }
            return View();
        }

        public ActionResult FindScore_1()
        {
            using(var dbContext = new AdScoreShowDbContext())
            {
                var result = dbContext.AdvertAireds.Max(a => a.Score_1.Value);

                ViewBag["Score_1"] = result;

                return View("HighestScore_1");
            }
            
        }

        public ActionResult FindScore_2()
        {
            using (var dbContext = new AdScoreShowDbContext())
            {
                var result = dbContext.AdvertAireds.Max(a => a.Score_2.Value);

                ViewBag["Score_2"] = result;

                return View("HighestScore2");
            }            
        }

        public ActionResult FindBestScore()
        {
            using (var dbContext = new AdScoreShowDbContext())
            {
                var result1 = dbContext.AdvertAireds.Max(a => a.Score_1.Value);
                var result2 = dbContext.AdvertAireds.Max(a => a.Score_2.Value);
                int result = 0;
                int score1 = result1 / 30;
                

                if( score1 >= result2)
                {
                    result = result1;
                }
                else
                {
                    result = result2;
                }

                ViewBag["BestScore"] = result;

                return View();
            }
        }
    }
}