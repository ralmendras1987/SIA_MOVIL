﻿//using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SIA_MOVIL_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // var corsHosts = new EnableCorsAttribute("http://localhost:100,http://cmpc-ds119.cmpc.cl:100", "*", "*");

           // config.EnableCors(corsHosts);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
