using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIA_MOVIL_API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Indicadores")]
    public class IndicadoresController : ApiController
    {
        [HttpPost]
        [Route("SP_LISTA_INDICADORES")]
        public HttpResponseMessage SP_LISTA_INDICADORES([FromBody] SIA_MOVIL_MODELO.VM_Indicadores DATA)
        {

            if (DATA is null)
            {

                DATA = new SIA_MOVIL_MODELO.VM_Indicadores();
                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error en solicitud";
                return Request.CreateResponse(HttpStatusCode.BadRequest, DATA);
            }



            SIA_MOVIL_MODELO.Indicadores_Metodos.SP_LISTA_INDICADORES(DATA);


            if (DATA.ERROR_ID == 0)
            {

                return Request.CreateResponse(HttpStatusCode.OK, DATA);

            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, DATA);

            }

        }

        [HttpPost]
        [Route("SP_LISTA_INDICADORES_DET")]
        public HttpResponseMessage SP_LISTA_INDICADORES_DET([FromBody] SIA_MOVIL_MODELO.VM_Indicadores_Det DATA)
        {

            if (DATA is null)
            {

                DATA = new SIA_MOVIL_MODELO.VM_Indicadores_Det();
                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error en solicitud";
                return Request.CreateResponse(HttpStatusCode.BadRequest, DATA);
            }



            SIA_MOVIL_MODELO.Indicadores_Metodos.SP_LISTA_INDICADORES_DET(DATA);


            if (DATA.ERROR_ID == 0)
            {

                return Request.CreateResponse(HttpStatusCode.OK, DATA);

            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, DATA);

            }

        }
    }
}
