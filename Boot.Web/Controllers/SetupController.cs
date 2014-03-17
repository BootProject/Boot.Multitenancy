#define DEBUG
#define TRACE
#undef TRACE

using Boot.Web.Models;
using Boot.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlServerCe;
using System.Data.SqlClient;

namespace Boot.Web.Controllers
{
    public class SetupController : Controller
    {

        SetupViewModel Model { get; set; }

        public SetupController()
        {
            Model = new SetupViewModel();
        }


        public ActionResult Index()
        {
            return View(Model);
        }


        [HttpPost]
        public ActionResult Index(SetupViewModel model)
        {
            Model.EntityNames.AddRange(model.EntityNames);

            dynamic con = null;

            switch (model.SelectedType)
            {
                case DbType.SqlCe:
                    con = new SqlCeConnection(model.Connectionstring);
                    break;
                case DbType.MySql5:
                    con = new MySql.Data.MySqlClient.MySqlConnection(model.Connectionstring);
                    break;
                case DbType.SqlServer2008:
                    con = new SqlConnection(model.Connectionstring);
                    break;
            }

            dynamic ex = null;
#if debug
            try{ con.Open(); } catch(Exception e)  {
                ex = e;
                goto TypeErrorDebug;
            }

            TypeErrorDebug: ModelState.AddModelError("Connectionstring", "The connectionstring seems invalid.\n" + ex.Message);
#else

            try { con.Open(); }
            catch
            {
                goto TypeError;
            }

//#line 48 "Error"
        TypeError: ModelState.AddModelError("Connectionstring", "The connectionstring seems invalid.\n");
#endif

            //#warning debug is defined at line 56

            return View(model);
        }

        public ActionResult Finish()
        {
            return View();
        }
    }
} 