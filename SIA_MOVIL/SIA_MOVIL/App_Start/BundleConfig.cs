using System.Web;
using System.Web.Optimization;

namespace SIA_MOVIL
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/Global.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css"));







            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                     "~/js/Login/LoginService.js","~/js/Login/Login.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                      "~/Content/Login.css"));

            bundles.Add(new ScriptBundle("~/bundles/Indicadores").Include(
                     "~/js/Indicadores/IndicadoresService.js", "~/js/Indicadores/Indicadores.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/Indicadores").Include(
                      "~/Content/Indicadores.css"));
        }
    }
}
