using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SIA_MOVIL_MODELO;

namespace SIA_MOVIL_WEBAPI.Providers
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var authToken = request.Headers.Authorization;
                var validToken = Metodos.ValidaToken(authToken.Parameter);
                if (validToken.Resultado == false)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                else
                {
                    if (!(bool)validToken.Elemento)
                    {
                        statusCode = HttpStatusCode.Unauthorized;
                    }
                }

                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }
    }
}