using CoopCrea.Cross.Http.Src;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Personas.Core.Dtos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Repositories
{
    public class MensajesRespuestaRepository : IMensajesRespuestaRepository
    {
        private readonly ILogsRepository<MensajesRespuestaRepository> _logger;
        private readonly IHttpClient _httpClient;
        protected readonly IConfiguration _configuration;

        public MensajesRespuestaRepository(ILogsRepository<MensajesRespuestaRepository> logger, IHttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

 
        public async Task<string> ObtenerTextoInfo(string idioma, string codigoEvento, string modulo)
        {
            try
            {
                var query = new Dictionary<string, string>()
                {
                    ["codigo"] = codigoEvento,
                    ["modulo"] = modulo,
                    ["lang"] = idioma
                };
                
                string url = $"{_configuration.GetSection("apiIdiomas:url").Value}{_configuration.GetSection("apiIdiomas:pointIdiomas").Value}";
                url = QueryHelpers.AddQueryString(url, query);
               
                
                MensajesIdioma mensajesIdioma = null;
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                

                if (response.Content != null)
                {
                    mensajesIdioma = JsonConvert.DeserializeObject<MensajesIdioma>(await response.Content.ReadAsStringAsync());
                }
                return mensajesIdioma.resultado;
            }
            catch (Exception exc)
            {
                return $"Event code #{codigoEvento}";
            }
        }
        
        // public async Task<string> ObtenerTextoInfo(string idioma, string codigoEvento, string modulo)
        // {
        //     try
        //     {
        //         var query = new Dictionary<string, string>()
        //         {
        //             ["servicio"] = modulo,
        //             ["tipo"] = (codigoEvento > 0 ? "msg" : "err"),
        //             ["codigo"] = codigoEvento.ToString(),
        //             ["lang"] = idioma
        //         };
        //
        //         string url = $"{_configuration.GetSection("apiIdiomas:url").Value}{_configuration.GetSection("apiIdiomas:pointIdiomas").Value}";
        //         url = QueryHelpers.AddQueryString(url, query);
        //
        //         MensajesIdioma mensajesIdioma = null;
        //         var response = await _httpClient.GetAsync(url);
        //         response.EnsureSuccessStatusCode();
        //
        //         if (response.Content != null)
        //         {
        //             mensajesIdioma = JsonConvert.DeserializeObject<MensajesIdioma>(await response.Content.ReadAsStringAsync());
        //         }
        //
        //         _logger.Informativo($"Servicio de idioma {mensajesIdioma.codigo}");
        //         return mensajesIdioma.resultado;
        //     }
        //     catch (Exception exc)
        //     {
        //         Console.WriteLine(exc);
        //         _logger.Error($"ObtenerTextoInfo => [EVENT CODE]: {codigoEvento}\n[LANG]: {idioma}\n [EXCEPTION]: {exc}");
        //         return $"Event code #{codigoEvento}";
        //     }
        //
        // }
    }
}
