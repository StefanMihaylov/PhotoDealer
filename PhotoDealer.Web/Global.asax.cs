namespace PhotoDealer.Web
{    
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using PhotoDealer.Web.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngineConfig.RegisterEngines();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
