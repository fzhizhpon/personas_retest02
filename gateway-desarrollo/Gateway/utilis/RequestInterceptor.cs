using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace Gateway.Api.utilis
{
    public class RequestInterceptor
    {
        private readonly RequestDelegate _next;

        public RequestInterceptor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string cliente = "Unknown",
                   ipPrivada = "Unknown",
                   ipPublica = "Unknown";

            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                ipPrivada = httpContext.Request.Headers["X-Forwarded-For"];
            else
                ipPrivada = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            try
            {
                if (httpContext.Request.Headers.ContainsKey("User-Agent"))
                {
                    var userAgent = httpContext.Request.Headers["User-Agent"];
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(userAgent);

                    cliente = $"{c.Device} - {c.OS}";
                }
                else
                {
                    cliente = "Unknown";
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot get client info: ", ex);
            }

            string header = (httpContext.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault());
            if (IPAddress.TryParse(header, out IPAddress ip))
            {
                ipPublica = ip.ToString();
            }

            httpContext.Request.Headers["IP-Publica"] = ipPublica;
            httpContext.Request.Headers["IP-Privada"] = ipPrivada;
            httpContext.Request.Headers["Cliente"] = cliente;
            httpContext.Request.Headers["Terminal"] = $"Cliente: {cliente}; IP Pub: {ipPublica}; IP Priv: {ipPrivada};";

            Stream originalResponseBody = httpContext.Response.Body;

            using (var memStream = new MemoryStream())
            {
                await _next(httpContext);

                memStream.Position = 0;
                await memStream.CopyToAsync(originalResponseBody);
            }
        }
    }
}
