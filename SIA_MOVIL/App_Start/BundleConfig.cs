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
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css"));

            //bundles.Add(new ScriptBundle("~/bundles/Login").Include(
            //         "~/js/Login/LoginService.js","~/js/Login/Login.js"
            //         ));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                      "~/Content/Login.css"));

            /************************************************************************************************/

            #region "ESTILOS"

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap.css",
                "~/Content/toastr.css",
                "~/Content/jquery-ui.css",
                "~/Content/css/principal.css"));

            bundles.Add(new StyleBundle("~/css/estacion").Include("~/Content/css/estacion.css"));
            bundles.Add(new StyleBundle("~/css/indicadores").Include("~/Content/css/indicadores.css"));
            bundles.Add(new StyleBundle("~/css/detalle_fiscalizaciones").Include("~/Content/css/detalle_fiscalizaciones.css"));

            #endregion

            #region "SCRIPTS"

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-3.5.1.min.js",
                    "~/Scripts/bootstrap.bundle.min.js",
                    "~/Scripts/97fa47432f.js",
                    "~/Scripts/feather.min.js",
                    "~/Scripts/Chart.min.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/toastr.js",
                    "~/Scripts/jquery-ui.js",
                    "~/Scripts/moment.min.js",
                    "~/Scripts/sweetalert.min.js",
                    "~/Scripts/crypto-js.min.js",
                    "~/Scripts/app/global.js"));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include("~/js/Login/Login.js"));
            bundles.Add(new ScriptBundle("~/js/estaciones").Include("~/Scripts/app/estaciones.js"));

            #endregion

            #region "MERGE"

            bundles.Add(new ScriptBundle("~/bundles/Indicadores").Include(
                     "~/js/Indicadores/IndicadoresService.js", 
                     "~/js/Indicadores/Indicadores.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/Indicadores").Include(
                      "~/Content/Indicadores.css"));

            #endregion

        }
    }
}
