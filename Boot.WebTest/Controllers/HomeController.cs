using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions;
using Boot.WebTest.Environment;

namespace Boot.WebTest.Controllers
{
    public class HomeController : Controller
    {
        TestViewModel Model = new TestViewModel();

        public ActionResult Index()
        {
            return View(Model);
        }
    }
}