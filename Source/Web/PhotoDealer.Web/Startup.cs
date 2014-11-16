[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(PhotoDealer.Web.Startup))]

namespace PhotoDealer.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
