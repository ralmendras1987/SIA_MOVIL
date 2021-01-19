using System.Web.Mvc;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using SIA_MOVIL.Models;
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
                var JwtToken = ValidateJwtToken(objSesion.TokenJWT);
                if (JwtToken == null)
                {
                    if (controlador != "Login")
                    {
                        MSession.FreeSession();
                        filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.ApplicationPath);
                    }
                }
            }
        }

        private JwtSecurityToken ValidateJwtToken(string tokenString)
        {
            try
            {
                var secretKey = ConfigurationManager.AppSettings["PrivateKey"];
                var urlWeb = ConfigurationManager.AppSettings["URL_WEB"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validation = new TokenValidationParameters()
                {
                    ValidAudience = urlWeb,
                    ValidIssuer = urlWeb,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    LifetimeValidator = CustomLifetimeValidator,
                    RequireExpirationTime = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuerSigningKey = true,
                };

                SecurityToken token;
                ClaimsPrincipal principal = handler.ValidateToken(tokenString, validation, out token);

                return (JwtSecurityToken)token;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}