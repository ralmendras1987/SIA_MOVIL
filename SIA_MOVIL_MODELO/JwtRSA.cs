using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace SIA_MOVIL_MODELO
{
    public class JwtRSA
    {
        public static string GenerateToken(string name, string id) 
        {
            var path_base = WebConfigurationManager.AppSettings["PATH.API"];

            //Password for public & private key file
            JWT.JWTUtils.RSAENCRYPTPASSORD = WebConfigurationManager.AppSettings["RSAENCRYPTPASSORD"];

            //public & private key file
            JWT.JWTUtils.RSAKEYPATH = Path.Combine(path_base, "RsaKeys\\rsa1.txt");

            //public key in XML format, the library copy in rsapub1.PEM the same key in PEM format
            JWT.JWTUtils.RSAPUBKEYPATH = Path.Combine(path_base, "RsaKeys\\rsapub1.xml");

            return JWT.JWTUtils.CreateToken(name, id, JWT.Managers.SecretType.RSAKey);
        }

        public static bool ValidateToken(string stringToken) 
        {
            var path_base = WebConfigurationManager.AppSettings["PATH.API"];

            StreamReader fileStream = new StreamReader(Path.Combine(path_base, "RsaKeys\\rsapub1.xml"));
            string pubkey = fileStream.ReadToEnd();
            var v = JWT.JWTUtils.VerifyToken(stringToken, pubkey, JWT.Managers.SecretType.RSAKey);

            return v.valid;
        }

    }
}
