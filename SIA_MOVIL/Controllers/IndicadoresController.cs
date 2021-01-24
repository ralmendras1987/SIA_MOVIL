using Newtonsoft.Json;
using RestSharp;
using SIA_MOVIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SIA_MOVIL.Controllers
{
    [AutorizeSession]
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        public ActionResult Index()
        {

            return View("~/Views/Indicadores/Index.cshtml");
        }

        public ActionResult Indicadores()
        {

            return View("~/Views/Indicadores/Index.cshtml");
        }

        [System.Web.Http.HttpPost]
        public ActionResult SP_LISTA_INDICADORES(string Data)
        {
            Response.TrySkipIisCustomErrors = true;
            try
            {

                SIA_MOVIL_MODELO.VM_Indicadores OBJ = JsonConvert.DeserializeObject<SIA_MOVIL_MODELO.VM_Indicadores>(Data);
                //DESCOMENTAR EN PROD
                //OBJ.SESSION = (SIA_MOVIL_MODELO.VM_Usuario)Session["UserSession"];
                //OBJ.SESSION.TOKEN = Session["TokenSession"].ToString();

                IRestResponse Req = Comun.ApiPOST(JsonConvert.SerializeObject(OBJ), Comun.URL + "Indicadores/", "SP_LISTA_INDICADORES");

                OBJ = JsonConvert.DeserializeObject<SIA_MOVIL_MODELO.VM_Indicadores>(Req.Content);

                Session["UserSession"] = OBJ.SESSION;
                Session["TokenSession"] = OBJ.SESSION.TOKEN;

                OBJ.SESSION.TOKEN = string.Empty;
                OBJ.SESSION.USER_DATA.PASS = string.Empty;

                return new JsonHttpStatusResult(OBJ, Req.StatusCode);
            }
            catch (Exception e)
            {

                return new JsonHttpStatusResult(new SIA_MOVIL_MODELO.VM_Indicadores { ERROR_ID = 1, ERROR_DSC = "Error solicitud" }, HttpStatusCode.BadRequest);

            }

        }

        [System.Web.Http.HttpPost]
        public ActionResult SP_LISTA_INDICADORES_DET(string Data)
        {
            Response.TrySkipIisCustomErrors = true;
            try
            {

                SIA_MOVIL_MODELO.VM_Indicadores_Det OBJ = JsonConvert.DeserializeObject<SIA_MOVIL_MODELO.VM_Indicadores_Det>(Data);

                //DESCOMENTAR EN PROD
                //OBJ.SESSION = (SIA_MOVIL_MODELO.VM_Usuario)Session["UserSession"];
                //OBJ.SESSION.TOKEN = Session["TokenSession"].ToString();

                IRestResponse Req = Comun.ApiPOST(JsonConvert.SerializeObject(OBJ), Comun.URL + "Indicadores/", "SP_LISTA_INDICADORES_DET");

                OBJ = JsonConvert.DeserializeObject<SIA_MOVIL_MODELO.VM_Indicadores_Det>(Req.Content);

                Session["UserSession"] = OBJ.SESSION;
                Session["TokenSession"] = OBJ.SESSION.TOKEN;

                OBJ.SESSION.TOKEN = string.Empty;
                OBJ.SESSION.USER_DATA.PASS = string.Empty;

                return new JsonHttpStatusResult(OBJ, Req.StatusCode);
            }
            catch (Exception e)
            {

                return new JsonHttpStatusResult(new SIA_MOVIL_MODELO.VM_Indicadores_Det { ERROR_ID = 1, ERROR_DSC = "Error solicitud" }, HttpStatusCode.BadRequest);

            }

        }
    }
}