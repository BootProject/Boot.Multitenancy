using log4net;
using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Reflection;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(IAppBuilder app)
        {
            log.Clear();
            log4net.Config.XmlConfigurator.Configure();

            ConfigureAuth(app);
        }
    }

    public static class LoggExtension
    {
        public static void Clear(this ILog log) 
        {
            try { 
                var f = new FileInfo(AppDomain.CurrentDomain.GetData("DataDirectory") + "\\BootLogger.log");
                var fileStream = f.OpenWrite();
                fileStream.SetLength(0); //Delete content on each startup.
                fileStream.Close();
            }
            catch { }
        }
    }
}
