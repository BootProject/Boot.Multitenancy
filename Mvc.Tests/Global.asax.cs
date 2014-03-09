using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Host = Boot.Multitenancy.BootHost;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            //Create some databases.
            //Extraxt database names from web.config and create them in app_data folder.
            (from configuration in Host.PreInit() select configuration)
                .ToList()
                    .ForEach(d => {
                        try{
                            new SqlCeEngine(d.Connectionstring).CreateDatabase();
                        } catch { }
                    });

 
            Host.Init();
            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
