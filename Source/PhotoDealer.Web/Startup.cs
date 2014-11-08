using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoDealer.Web.Startup))]
namespace PhotoDealer.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
