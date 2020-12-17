using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_MOVIL_MODELO
{
    public class Login
    {
        public string usuario = "cmpc";
        public string password = "20.cmpc.!";

    }
    public class Token
    {
        public string status = string.Empty;
        public string value = string.Empty;

    }
    public class VM_Login
    {

        public GenesysJWT.Usuario USER = new GenesysJWT.Usuario();
        public Filtros FILTROS = new Filtros();

        public string ERROR_DSC = string.Empty;

    }



    public class Filtros
    {
        public string PLANTA = string.Empty;
        public string ESTACION = string.Empty;
        public string FECHA_INI = string.Empty;
        public string FECHA_FIN = string.Empty;

    }


    public class Error
    {
        public string ID = string.Empty;
        public string PLANTA = string.Empty;
        public string ESTACION = string.Empty;
        public string ETAPA = string.Empty;
        public string EXCEPCION = string.Empty;
        public string MENSAJE = string.Empty;

    }

    public class Historico
    {
        public string codigo = string.Empty;


        public Filtros filtros = new Filtros();

        public List<Resultado> resultado = new List<Resultado>();
        public List<Consulta> consulta = new List<Consulta>();

        public Login LOGIN = new Login();
        public Token TOKEN = new Token();
        public GenesysJWT.Usuario USER = new GenesysJWT.Usuario();
        public List<string> ERRORES;

    }



    public class Consulta
    {
        public string dispositivoId = string.Empty;
        public string plantaid = string.Empty;
        public string estacionid = string.Empty;
        public string estampaTiempoInicial = string.Empty;
        public string estampaTiempoFinal = string.Empty;
        public string estacionActiva = string.Empty;
        public string tipoMedicion = string.Empty;
    }
    public class Resultado
    {
        public string dispositivoId = string.Empty;
        public List<Parametros> parametros = new List<Parametros>();

    }
    public class Parametros
    {
        public string nombre = string.Empty;
        public string valor = string.Empty;
        public string unidad = string.Empty;
        public string estampaTiempo = string.Empty;
        public string estado = string.Empty;

        public string estacionid = string.Empty;
        public string plantaid = string.Empty;

        public string estacionActiva = string.Empty;
    }


    // PARA CARGAR UNA VARIABLE ESPECIFICA DESDE API.



    public class VM_LoginHst
    {

        public GenesysJWT.Usuario USER = new GenesysJWT.Usuario();
        public FiltrosHst FILTROS = new FiltrosHst();

        public string ERROR_DSC = string.Empty;

    }

    public class FiltrosHst
    {
        public string PLANTA = string.Empty;
        public string ESTACION = string.Empty;
        public string FECHA_INI = string.Empty;
        public string FECHA_FIN = string.Empty;
        public string VARIABLE = string.Empty;
        public string TIPO = string.Empty;
    }

    public class HistoricoHst
    {
        public string codigo = string.Empty;


        public FiltrosHst filtros = new FiltrosHst();

        public List<Resultado> resultado = new List<Resultado>();
        public List<ConsultaHst> consulta = new List<ConsultaHst>();

        public Login LOGIN = new Login();
        public Token TOKEN = new Token();
        public GenesysJWT.Usuario USER = new GenesysJWT.Usuario();
        public List<string> ERRORES;
       
    }

    public class ConsultaHst
    {
        public string dispositivoId = string.Empty;
        public string plantaid = string.Empty;
        public string estacionid = string.Empty;
        public string estampaTiempoInicial = string.Empty;
        public string estampaTiempoFinal = string.Empty;
        public string estacionActiva = string.Empty;
        public string tipoMedicion = string.Empty;
        public string nombre = string.Empty;

        //public List<ParametrosHst> parametros = new List<ParametrosHst>();
    }

    
    public class ParametrosHst
    {

        public string nombre = string.Empty;
        

    }

}

