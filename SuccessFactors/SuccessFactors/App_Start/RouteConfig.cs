﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuccessFactors
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "ExternalHome", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 "Account",
                "{id}/{controller}/{action}",
                defaults: new { controller = "Account", action = "ValidateExtUser", id = UrlParameter.Optional }
            );
        }
    }
}
