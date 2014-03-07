using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions;
using Boot.WebTest.Environment;
using Boot.Multitenancy.Configuration;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;

namespace Boot.WebTest.Controllers
{
    public class HomeController : Controller
    {
        TestViewModel Model = new TestViewModel();

        public ActionResult Index()
        {
            return View(Model);
        }

        public ActionResult ReadConfig()
        {
            

            return View();
        }
    }        
    


    public class ConfigsModel
    {
        public ConfigsModel()
        {
            Databases = new DatabaseCollection();
        }

        public DatabaseCollection Databases { get; set; }
    }
}