using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App = Boot.Multitenancy;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Lets create some databases.
            //Extraxt database names from web.config and create them in app_data folder.
            (from db in App.BootHost.InitCreate() select db)
                .ToList()
                    .ForEach(d => {
                        try{
                            new SqlCeEngine(d.Connectionstring).CreateDatabase();
                        } catch { }
                    });

            App.BootHost.Init();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
