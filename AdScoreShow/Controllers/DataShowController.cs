using AdScoreShow.Repository;
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

            }
            return View();
        }
    }
}