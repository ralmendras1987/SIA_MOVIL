using Newtonsoft.Json;
using RestSharp;
using SIA_MOVIL_MODELO;
using SIA_MOVIL_MODELO.DTO;
using System;

namespace SIA_MOVIL.Models
{
    public class MSession
    {
        /// <summary>
		/// Método que realiza el registro la session del usuario
		/// </summary>
        public static void RegisterSession(DTOSessionUsuario sess)
        {
            System.Web.HttpContext.Current.Session["UsuarioSess"] = sess;
        }

        /// <summary>
		/// Método que checkea los recursos del objeto de sesion del usuario
		/// </summary>
        public static bool CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["UsuarioSess"] != null)
                return true;
            else
                return false;
        }

        /// <summary>
		/// Método que devuelve el objeto de sesion del usuario
		/// </summary>
        public static object ReturnSessionObject()
        {
            if (CheckSession())
                return System.Web.HttpContext.Current.Session["UsuarioSess"];
            else
                return null;
        }

        /// <summary>
		/// Método que devuelve el objeto de sesion del usuario
		/// </summary>
        public static bool isAjaxCall()
        {
            if (System.Web.HttpContext.Current.Request.Headers["Authorization"] != null &&
                !string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Request.Headers["Authorization"]))
                return true;
            else
                return false;
        }

        /// <summary>
		/// Método que libera recursos del objeto de sesion del usuario
		/// </summary>
        public static void FreeSession()
        {
            System.Web.HttpContext.Current.Session["UsuarioSess"] = null;
            System.Web.HttpContext.Current.Session.RemoveAll();
            System.Web.HttpContext.Current.Session.Clear();
        }
    }
}