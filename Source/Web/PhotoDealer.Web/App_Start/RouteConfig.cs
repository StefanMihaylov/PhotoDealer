using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoDealer.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GetAllCategoryGroups",
                url: "GetAllCategoryGroups/{id}",
                defaults: new
                {
                    controller = "CategoryGroup",
                    action = "GetAll",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "PhotoDealer.Web.Controllers" }
            );

            routes.MapRoute(
               name: "GetAllCategories",
               url: "GetAllCategories/{id}",
               defaults: new
               {
                   controller = "Category",
                   action = "GetAll",
                   id = UrlParameter.Optional
               },
               namespaces: new[] { "PhotoDealer.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PhotoDealer.Web.Controllers" }
            );
        }
    }
}
