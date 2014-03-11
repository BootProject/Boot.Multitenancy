using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boot.ModelFactory;
using Boot.Models;

namespace Boot.Controllers
{
    public class HomeController : Controller
    {
        //TestModel
        PageViewModel Model;

        public HomeController()
        {
            Model = new PageViewModel();
        }

        public ActionResult Index()
        {
            return View(Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(Model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(Model);
        }
    }
}