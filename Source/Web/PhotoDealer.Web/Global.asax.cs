namespace PhotoDealer.Web
{
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var autoMapper = new AutoMapperConfig(Assembly.GetExecutingAssembly());
            autoMapper.Execute();
            ViewEngineConfig.RegisterEngines();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
