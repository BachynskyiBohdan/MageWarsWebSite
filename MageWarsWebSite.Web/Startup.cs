using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MageWarsWebSite.Web.Startup))]
namespace MageWarsWebSite.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
