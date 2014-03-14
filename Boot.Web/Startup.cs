using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Boot.Web.Startup))]
namespace Boot.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            BootConfig.Init();

            ConfigureAuth(app);
        }
    }
}
