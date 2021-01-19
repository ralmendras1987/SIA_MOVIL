using SIA_MOVIL_MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIA_MOVIL_WEBAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Estaciones")]
    public class EstacionesController : ApiController
    {
        [HttpPost]
        [Route("ConsultaEstaciones")]
        public IHttpActionResult Post1([FromBody] Dictionary<string, object> param)
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                string  usuario = param["USUARIO"].ToString();

                respuesta = Metodos.ConsultaEstaciones(usuario);
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

        /// <summary>
        /// Petición que devuelve una lista de las solicitudes ingresadas
        /// </summary>
        /// <param name="param">Dictionary<string, object></param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        [Route("ConsultaDetalleEstacion")]
        public IHttpActionResult Post2([FromBody] Dictionary<string, object> param)
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                string planta = param["PLANTA"].ToString(),
                       fecha = param["FECHA"].ToString(),
                       usuario = param["USUARIO"].ToString();

                int estacion = Convert.ToInt32(param["ESTACION"]),
                    rango = Convert.ToInt32(param["RANGO"]);

                var datosGrafico = Metodos.ConsultaGraficoEstacion(planta, estacion, fecha, rango, usuario);
                var datosTabla = Metodos.ConsultaTablaEstacion(planta, estacion, fecha, rango, usuario);

                respuesta.Elemento = new Dictionary<string, object>() {
                    { "GRAFICO", (datosGrafico.Resultado == true) ? datosGrafico.Elemento : null },
                    { "TABLA", (datosTabla.Resultado == true) ? datosTabla.Elemento : null }
                };

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

        [HttpPost]
        [Route("ConsultaDetalleVariable")]
        public IHttpActionResult Post3([FromBody] Dictionary<string, object> param)
        {
            DTORespuesta respuesta = new DTORespuesta();
            try
            {
                string planta = param["PLANTA"].ToString(),
                       fecha = param["FECHA"].ToString(),
                       usuario = param["USUARIO"].ToString();

                int variable = Convert.ToInt32(param["VARIABLE"]), 
                    estacion = Convert.ToInt32(param["ESTACION"]),
                    rango = Convert.ToInt32(param["RANGO"]);

                respuesta = Metodos.ConsultaDetalleVariable(planta, variable, estacion, fecha, rango, usuario);

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
