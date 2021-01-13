using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIA_MOVIL.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        public ActionResult Index()
        {
            Session["UserSession"] = "";
            Session["TokenSession"] = "";
            return View("~/Views/Indicadores/Index.cshtml");
        }

        public ActionResult Indicadores()
        {
            Session["UserSession"] = "";
            Session["TokenSession"] = "";
            return View("~/Views/Indicadores/Index.cshtml");
        }
    }
}