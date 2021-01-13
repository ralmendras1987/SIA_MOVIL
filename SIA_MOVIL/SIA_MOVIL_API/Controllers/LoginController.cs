using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIA_MOVIL_API.Controllers
{
    public class LoginController : ApiController
    {


        [HttpPost]
        public HttpResponseMessage IniciarSesion([FromBody] SIA_MOVIL_MODELO.VM_Usuario SESSION)
        {

            if (SESSION is null)
            {

                SESSION = new SIA_MOVIL_MODELO.VM_Usuario();
                SESSION.ERROR_ID = 1;
                SESSION.ERROR_DSC = "Error en solicitud";
                return Request.CreateResponse(HttpStatusCode.Unauthorized, SESSION);
            }



            SIA_MOVIL_MODELO.Metodos.IniciaSesion(SESSION);


            if (SESSION.ERROR_ID == 0)
            {

                return Request.CreateResponse(HttpStatusCode.OK, SESSION);

            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.Unauthorized, SESSION);

            }

        }

    }
}
