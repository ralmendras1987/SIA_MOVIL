using SIA_MOVIL_WEBAPI.Providers;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SIA_MOVIL_WEBAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string WEBSITES = WebConfigurationManager.AppSettings["ALLOW_WESITES_CORS"];
            var corsHosts = new EnableCorsAttribute(WEBSITES, "*", "*");
            config.EnableCors(corsHosts);
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
