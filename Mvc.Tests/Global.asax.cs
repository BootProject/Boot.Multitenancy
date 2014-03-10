using Boot.Multitenancy.Configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Host = Boot.Multitenancy.BootHost;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            //Create some databases.
            //Extraxt database names from web.config and create them in app_data folder.
            log.Debug("Global application start");
            (from configuration in Host.PreInit() select configuration)
                .ToList()
                    .ForEach(d => {
                        try{
                            new SqlCeEngine(d.Connectionstring).CreateDatabase();
                        } catch (Exception ex)
                        {
                            log.Debug(ex.Message, ex);
                        }
                    });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Host.Init();
            log.Debug("Global application end");
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            log.Debug("Global Host.Init start");
        }
    }
}
