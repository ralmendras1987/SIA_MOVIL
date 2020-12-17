using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using SIA_MOVIL_MODELO;
using Microsoft.IdentityModel.Tokens;

namespace GenesysJWT
{
    public class Usuario {

        public string USER = string.Empty;
        public string TOKEN = string.Empty;
        public string KEY = string.Empty;
        public string STATUS = string.Empty;
        public string SESSION_ID = string.Empty;


    }


    public class JWT
    {

        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        public static string Key = "U0lBX01PVklMX0FQSUtleQ==";
  
        public static bool Testing = true;

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }


        public string GeneraToken(Usuario USER)
        {

            DateTime expiracion = DateTime.UtcNow.AddMinutes(Comun._TIEMPO_EXPIRA_SESION());

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, USER.USER), new Claim(ClaimTypes.Expiration, expiracion.ToString())}),
                Expires = expiracion,
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public string GeneraToken(Usuario USER, int minutos)
        {

            DateTime expiracion = DateTime.UtcNow.AddMinutes(minutos);

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, USER.USER), new Claim(ClaimTypes.Expiration, expiracion.ToString())}),
                Expires = expiracion,
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }


        public bool ValidaToken(Usuario USER, int minutos)
        {


            if (USER.KEY == Key) {
                return true;
            }

            if (USER.TOKEN is null || USER.TOKEN == string.Empty)
            {
                return false;
            }



            string username = null;

            ClaimsPrincipal principal = GetPrincipal(USER.TOKEN);
            if (principal == null)
                return false;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;


            Claim expiraClaim = identity.FindFirst(ClaimTypes.Expiration);
            string expira = null;
            expira = expiraClaim.Value;
            DateTime EXPIRACION = DateTime.Parse(expira);
            //DateTime AHORA = DateTime.UtcNow.AddMinutes(minutos);
            DateTime AHORA = DateTime.UtcNow;



            if (EXPIRACION > AHORA && username == USER.USER)
            {

                return true;

            }
            else
            {

                return false;


            }



        }

    }


}
