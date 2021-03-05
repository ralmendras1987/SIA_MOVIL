using System.Web.Mvc;
using SIA_MOVIL.Models;
using SIA_MOVIL_MODELO;
using SIA_MOVIL_MODELO.DTO;

namespace SIA_MOVIL
{
    public class AutorizeSession : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string accion = filterContext.ActionDescriptor.ActionName;

            DTOSessionUsuario objSesion = (DTOSessionUsuario)MSession.ReturnSessionObject();

            if (objSesion == null)
                if (!MSession.isAjaxCall())
                {
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.ApplicationPath);
                }
                else
                    filterContext.Result = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new HttpUnauthorizedResult("Not authorized") };
            else
            {
                var validToken = Metodos.ValidaToken(objSesion.TokenJWT);
                if (validToken.Resultado == false)
                {
                    if (controlador != "Login")
                    {
                        MSession.FreeSession();
                        filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.ApplicationPath);
                    }
                }
                else
                {
                    if (!(bool)validToken.Elemento)
                    {
                        if (controlador != "Login")
                        {
                            MSession.FreeSession();
                            filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.ApplicationPath);
                        }
                    }
                }
            }
        }
    }
}