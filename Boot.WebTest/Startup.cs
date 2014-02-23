using Boot.WebTest.Environment;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Boot.WebTest.Startup))]
namespace Boot.WebTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Host.Init();
        }
    }
}
