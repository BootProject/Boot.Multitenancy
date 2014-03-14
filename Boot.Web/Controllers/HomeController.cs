using Boot.ModelFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Host = Boot.Multitenancy.SessionManager.SessionFactoryHostContainer;
using Boot.Multitenancy.Extensions;
using Boot.Multitenancy.SessionManager;
using Boot.Web.Models;

namespace Boot.Web.Controllers
{
    public class HomeController : Controller
    {
        public PageViewModel Model { get; set; }

        public HomeController()
        {
            new SetupModel().CreateIfNotExist();
            Model = new PageViewModel();
        }

        public ActionResult Index()
        {
            return View(Model);
        }

        public ActionResult About()
        {
            return View(Model);
        }

        public ActionResult Contact()
        {
            return View(Model);
        }
    }
}