using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Personas.Core.Dtos.App;

namespace Personas.Application.Utils
{
    public static class JwtAyuda
    {
        public static async Task<InformacionToken> obtenerInformacioToken(HttpContext httpContext)
        {
            InformacionToken infoToken = new InformacionToken
            {
                codigoAgencia = int.Parse(await JwtAyuda.GetClaim(httpContext, "codigoAgencia")),
                codigoPeriodo = int.Parse(await JwtAyuda.GetClaim(httpContext, "codigoPeriodo")),
                codigoUsuario = int.Parse(await JwtAyuda.GetClaim(httpContext, "codigoUsuario")),
                codigoRol = int.Parse(await JwtAyuda.GetClaim(httpContext, "codigoRol")),
                usuario = await JwtAyuda.GetClaim(httpContext, "usuario"),
                navegador = await JwtAyuda.GetClaim(httpContext, "navegador"),
                ipPrivada = await JwtAyuda.GetClaim(httpContext, "ipPrivada"),
                ipPublica = await JwtAyuda.GetClaim(httpContext, "ipPublica")
            };

            return infoToken;
        }

        /*private static async Task<string> GetToken(HttpContext httpContext)
        {
            var jwt = await Task.Run(() => httpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            return jwt;
        }*/

        private static async Task<IEnumerable<Claim>> GetTokenInformation(HttpContext httpContext)
        {
            var jwt = httpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }

        private static async Task<string> GetClaim(HttpContext httpContext, string claimName)
        {
            var token = await GetTokenInformation(httpContext);
            var tokenList = token.ToList();
            string claimValue = "";

            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Type.ToLower() == claimName.ToLower())
                {
                    claimValue = tokenList[i].Value;
                    return claimValue;
                }
            }

            return claimValue;
        }
    }
}

