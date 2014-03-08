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
            var conf = DatabaseCollectionReader.conf;
            var model = new ConfigsModel { Databases = conf.Databases };

            return View(model);
        }
    }        
    


    public class ConfigsModel
    {
        public DatabaseCollection Databases { get; set; }
    }
}