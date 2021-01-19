using System;
using System.Collections.Generic;

namespace SIA_MOVIL_MODELO
{
    public class Usuario
    {

        public string USER = string.Empty;
        public string PASS = string.Empty;
        public string NOMBRE = string.Empty;
        public string CORREO = string.Empty;
        public string TELEFONO = string.Empty;

        public string ROL = string.Empty;
        public string SESSION_ID = string.Empty;

        public int ERROR_ID = 0;
        public String ERROR_DSC = string.Empty;

    }

    public class Roles
    {
        public string CODIGO = string.Empty;
        public string DESCRIPCION = string.Empty;

    }

    public class VM_Usuario
    {

        public string TOKEN = string.Empty;
        public string KEY = string.Empty;

        public Usuario USER_DATA = new Usuario();
        public List<Roles> ROLES = new List<Roles>();

        public int ERROR_ID = 0;
        public String ERROR_DSC = string.Empty;

    }

    public class Token
    {
        public string status = string.Empty;
        public string value = string.Empty;

    }

    public class DTORespuesta
    {
        public DTORespuesta() {
            Resultado = true;
            IsError = false;
        }
        public bool? Resultado { get; set; }
        public bool? IsError { get; set; }
        public string Mensaje { get; set; }
        public object Elemento { get; set; }
    }
}

