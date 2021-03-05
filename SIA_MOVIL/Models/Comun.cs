using RestSharp;

namespace SIA_MOVIL.Models
{
    public class Comun
    {
        //public const string URL = "http://localhost:9100/PortalProveedoresAPI/api/";
        //public const string URL = "http://localhost:9770/SIA_MOVIL_API/api/";
        //public const string URL = "http://localhost:44344/api/";
        //public const string URL = "https://compramaderas.cmpc.cl/PortalProveedoresAPI/api/";
        public const string URL = "http://localhost:9770/SIA_MOVIL_WEBAPI/api/";

        public static IRestResponse ApiPOST(string Data, string URL, string Metodo)
        {

            var client = new RestClient(URL);

            RestRequest request = new RestRequest(Metodo, Method.POST, DataFormat.Json);
            request.Timeout = 1000 * 600;
            request.AddParameter("application/json", Data, ParameterType.RequestBody);

            return client.Post(request); 

           
        }

        public static IRestResponse ApiGET(string Data, string URL, string Metodo)
        {

            var client = new RestClient(URL);

            RestRequest request = new RestRequest(Metodo, Method.GET, DataFormat.Json);

            request.AddParameter("application/json", Data, ParameterType.RequestBody);

            return client.Get(request);


        }

    }
}