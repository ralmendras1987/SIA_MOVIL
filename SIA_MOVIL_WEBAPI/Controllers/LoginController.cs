using System;
using System.Web.Http;
using SIA_MOVIL_MODELO;

namespace SIA_MOVIL_WEBAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("ValidarLogin")]
        public IHttpActionResult Post1([FromBody] VM_Usuario SESSION)
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                if (SESSION == null)
                {
                    respuesta.Resultado = false;
                    respuesta.Mensaje = "Usuario / Contraseña incorrectas";
                }
                else
                {
                    Metodos.IniciaSesion(SESSION);
                    if (SESSION.ERROR_ID == 0)
                        respuesta.Elemento = SESSION;
                    else
                    {
                        respuesta.Resultado = false;
                        respuesta.Mensaje = SESSION.ERROR_DSC;
                    }
                }

                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Resultado = false;
                respuesta.IsError = true;
                respuesta.Mensaje = String.Format("Error en solicitud: {0}", ex.Message);
                return Json(respuesta);
            }
        }
    }
}
