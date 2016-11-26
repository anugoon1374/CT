using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Beyond.Ct.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region routing configurations

            // canonicalize urls
            routes.LowercaseUrls = true;
            routes.AppendTrailingSlash = false;

            // turns off the unnecessary file exists check
            //routes.RouteExistingFiles = true;

            #endregion routing configurations

            #region white-list routes

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #endregion white-list routes

            #region black-list routes

            // Ignore axd files such as asset, image, sitemap etc,.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ignore text, html, files.
            routes.IgnoreRoute("{file}.txt");
            routes.IgnoreRoute("{file}.htm");
            routes.IgnoreRoute("{file}.html");

            // Ignore the assets directory which contains images, js, css & html
            routes.IgnoreRoute("assets/{*pathInfo}");

            // Ignore the error directory which contains error pages
            routes.IgnoreRoute("error/{*pathInfo}");

            // Exclude favicon (google toolbar request gif file as fav icon which is weird)
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            #endregion black-list routes
        }
    }
}
