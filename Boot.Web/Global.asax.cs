using Boot.Multitenancy.Configuration;
using log4net;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace Boot
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {

            log.Debug("Global application start");

            BootHostWrapper.Init();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            log.Debug("Global application end");
        }

        public void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            log.Debug(ex);
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            log.Debug("Global BeginRequest start");
        }
    }
}
