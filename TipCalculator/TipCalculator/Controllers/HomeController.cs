using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TipCalculator.Models;

namespace TipCalculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Tip(Tip model)
        {
            return View("TipResult", model);
        }

        [HttpPost]
        public ActionResult Index(Tip model)
        {
            if (ModelState.IsValid)
            {
                return View("TipResult", model);
            }
            else
            {
                return View("Index", model);
            }
        }
    }
}