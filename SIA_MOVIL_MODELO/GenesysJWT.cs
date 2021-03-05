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
using Newtonsoft.Json;

namespace GenesysJWT
{
    public class JWT
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        public static string Key = "U0lBX01PVklMX0FQSUtleQ==";

        public static bool Testing = true;

        public static string SecAlg = SecurityAlgorithms.HmacSha256Signature;
        //public static string SecAlg2 = SecurityAlgorithms.RsaSha512Signature;

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

        public static string GeneraToken(VM_Usuario DATA)
        {

            DateTime expiracion = DateTime.UtcNow.AddMinutes(Comun._TIEMPO_EXPIRA_SESION());

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(DATA.USER_DATA)), new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(DATA.ROLES)) , new Claim(ClaimTypes.Expiration, expiracion.ToString())}),
                Expires = expiracion,
                SigningCredentials = new SigningCredentials(securityKey, SecAlg)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static string GeneraToken(VM_Usuario DATA, int minutos)
        {

            DateTime expiracion = DateTime.UtcNow.AddMinutes(minutos);

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(DATA.USER_DATA)), new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(DATA.ROLES)) , new Claim(ClaimTypes.Expiration, expiracion.ToString())}),
                Expires = expiracion,
                SigningCredentials = new SigningCredentials(securityKey, SecAlg)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static bool ValidaToken(VM_Usuario DATA, int minutos)
        {


            if (DATA.TOKEN is null || DATA.TOKEN == string.Empty)
            {

                return false;

            }


            ClaimsPrincipal principal = GetPrincipal(DATA.TOKEN);
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
            Claim userData = identity.FindFirst(ClaimTypes.UserData);

            Claim rolesData = identity.FindFirst(ClaimTypes.Role);


            Claim expiraClaim = identity.FindFirst(ClaimTypes.Expiration);
            string expira = null;
            expira = expiraClaim.Value;
            DateTime EXPIRACION = DateTime.Parse(expira);
            DateTime AHORA = DateTime.UtcNow;


            DATA.TOKEN = null;

            if (EXPIRACION > AHORA && JsonConvert.DeserializeObject<Usuario>(userData.Value) == DATA.USER_DATA && JsonConvert.DeserializeObject<List<Roles>>(rolesData.Value) == DATA.ROLES)
            {

                return true;

            }
            else
            {

                return false;


            }



        }

        public static string GenerateTokenJwt(string username)
        {
            // appsetting for Token JWT

            var secretKey = ConfigurationManager.AppSettings["PrivateKey"];
            var urlWeb = ConfigurationManager.AppSettings["URL_WEB"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecAlg);
            //var signingCredentials = new RSA(securityKey, SecAlg);
            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });
            try
            {             // create token to the user
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                    audience: urlWeb,
                    issuer: urlWeb,
                    subject: claimsIdentity,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                    signingCredentials: signingCredentials);

                var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
                return jwtTokenString;
            }
            catch (Exception e)
            {
                throw e;

            }


        }
    }

}
