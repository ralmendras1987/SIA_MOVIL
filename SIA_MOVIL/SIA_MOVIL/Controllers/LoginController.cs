using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SIA_MOVIL.Models;
using System.Net;

namespace SIA_MOVIL_API.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Session["UserSession"] = "";
            Session["TokenSession"] = "";
            return View("~/Views/Login/Login.cshtml");
        }

        public ActionResult Login()
        {
            Session["UserSession"] = "";
            Session["TokenSession"] = "";
            return View("~/Views/Login/Login.cshtml");
        }











        [System.Web.Http.HttpPost]
        public ActionResult IniciarSesion(string Data)
        {
            Response.TrySkipIisCustomErrors = true;
            try
            {
                IRestResponse Req = Comun.ApiPOST(Data, Comun.URL + "Login/", "IniciarSesion");

                SIA_MOVIL_MODELO.VM_Usuario SESSION = JsonConvert.DeserializeObject<SIA_MOVIL_MODELO.VM_Usuario>(Req.Content);

                if (SESSION.ERROR_ID != 0)
                {

                    return new JsonHttpStatusResult(new SIA_MOVIL_MODELO.Usuario { ERROR_ID = 1, ERROR_DSC = "Usuario no existe, valide datos" }, HttpStatusCode.BadRequest);
                }
                SESSION.USER_DATA.PASS = "";

                Session["UserSession"] = SESSION;
                Session["TokenSession"] = SESSION.TOKEN;

                Session.Timeout = 480;

                SESSION.TOKEN = "";


                return new JsonHttpStatusResult(SESSION, Req.StatusCode);
            }
            catch (Exception e)
            {

                return new JsonHttpStatusResult(new SIA_MOVIL_MODELO.Usuario { ERROR_ID = 1, ERROR_DSC = "Error solicitud" }, HttpStatusCode.BadRequest);
            }

        }



    }
}