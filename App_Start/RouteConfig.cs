using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MJC_Blogs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CommentId",
                url: "CommentReor-24-Tyqle/Del/{id}",
                defaults: new { controller = "Comments", action = "Delete", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "NewSlug",
                url: "BlogPost/Details/{slug}",
                defaults: new { controller = "Blogs", action = "details", slug = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Blogs", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blogs", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
