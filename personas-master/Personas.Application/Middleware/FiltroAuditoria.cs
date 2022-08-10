using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personas.Application.Utils;
using Personas.Core.App;
using Personas.Core.Dtos.App;
using Personas.Core.Interfaces.IRepositories;

namespace Personas.Application.Middleware
{
    public class FiltroAuditoria : IActionFilter
    {
        private string _endPoint;
        private InformacionToken _infoToken;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<FiltroAuditoria> _logger;

        public FiltroAuditoria(ConfiguracionApp config, ILogsRepository<FiltroAuditoria> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            object dto = context.ActionArguments;
            string entrada = System.Text.Json.JsonSerializer.Serialize(dto);

            try
            {
                _infoToken = await JwtAyuda.obtenerInformacioToken(context.HttpContext);
            }
            catch (Exception exc)
            {
                _logger.Error($"========= Error OnActionExecuting al obtener informacion del Token. ======= {exc}");
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.Headers.Clear();
                context.Result = new EmptyResult();
                return;
            }

            _endPoint = context.HttpContext.Request.Path;
            _config.guid = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers["lang"]))
            {
                _config.Idioma = context.HttpContext.Request.Headers["lang"];
            } else {
                _config.Idioma = "es";
            }

            _config.Modulo = "personas";
            _config.codigoUsuarioRegistra = _infoToken.codigoUsuario;
            _config.codigoAgencia = _infoToken.codigoAgencia;

            if (HttpMethods.IsGet(context.HttpContext.Request.Method))
            {
                return;
            }

            // Obtener IP's y navegador
            string ipPublica = context.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"],
                   ipPrivada = context.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"],
                   navegador = context.HttpContext.Request.Headers["User-Agent"];

            if (string.IsNullOrEmpty(ipPrivada))
            {
                ipPublica = context.HttpContext.Request.Headers["REMOTE_ADDR"];
                ipPrivada = context.HttpContext.Request.Headers["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(navegador))
            {
                ipPublica = "Navegador no enviado";
            }
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            string response = null;

            if (context.Result != null)
            {
                string jsonString = JsonConvert.SerializeObject(context.Result);
                JObject rest = JsonConvert.DeserializeObject<dynamic>(jsonString);
                if (rest["Value"] != null)
                {
                    response = rest["Value"].ToString();
                }
            }

            if (HttpMethods.IsGet(context.HttpContext.Request.Method))
            {
                return;
            }

            // Obtener IP's y navegador
            string ipPublica = context.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"],
                   ipPrivada = context.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"],
                   navegador = context.HttpContext.Request.Headers["User-Agent"];

            if (string.IsNullOrEmpty(ipPrivada))
            {
                ipPublica = context.HttpContext.Request.Headers["REMOTE_ADDR"];
                ipPrivada = context.HttpContext.Request.Headers["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(navegador))
            {
                ipPublica = "Navegador no enviado";
            }
        }

    }
}
