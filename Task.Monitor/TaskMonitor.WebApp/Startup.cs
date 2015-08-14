using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMonitor.WebApp.Startup))]
namespace TaskMonitor.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
