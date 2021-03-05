using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SIA_MOVIL.Models;
using System.Net;
using SIA_MOVIL_MODELO.DTO;
using SIA_MOVIL_MODELO;

namespace SIA_MOVIL_API.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SeteaTokenSession(Dictionary<string, object> data)
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                MSession.RegisterSession(new DTOSessionUsuario() {
                    TokenJWT = data["TOKEN"].ToString()
                });

                //IRestResponse Req = SIA_MOVIL.Models.Comun.ApiPOST(JsonConvert.SerializeObject(data["TOKEN"]), SIA_MOVIL.Models.Comun.URL + "Login/", "ValidarToken");
                //var OBJ = Req.Content;

                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                respuesta.IsError = true;
                respuesta.Resultado = false;
                respuesta.Mensaje = ex.Message;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = ex.Message.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\v", "").Replace("\f", "").ToString();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = respuesta };
        }

        [HttpGet]
        public JsonResult CerrarSesion()
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                MSession.FreeSession();
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                respuesta.IsError = true;
                respuesta.Resultado = false;
                respuesta.Mensaje = ex.Message;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = ex.Message.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\v", "").Replace("\f", "").ToString();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = respuesta };
        }

        [HttpGet]
        public JsonResult GetClaveEncript()
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                respuesta.Elemento = System.Web.Configuration.WebConfigurationManager.AppSettings["PrivateKey"];
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                respuesta.IsError = true;
                respuesta.Resultado = false;
                respuesta.Mensaje = ex.Message;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = ex.Message.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\v", "").Replace("\f", "").ToString();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = respuesta };
        }

    }
}