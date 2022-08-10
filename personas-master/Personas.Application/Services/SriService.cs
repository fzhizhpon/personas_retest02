using Newtonsoft.Json;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Sri;
using Personas.Core.Entities.Sri;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class SriService : ISriService
    {

        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<SriService> _logger;
        protected readonly IMensajesRespuestaRepository _textoInfoService;


        public SriService(
            ConfiguracionApp config,
            ILogsRepository<SriService> logger,
            IMensajesRespuestaRepository textoInfoService
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
        }

        public async Task<Respuesta> ObtenerContribuyenteConsolidado(ObtenerContribuyenteConsolidadoDto dto)
        {
            string codigoEvento = SriEventos.OBTENER_CONSOLIDAD_CONTRIBUYENTE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
            IList<ConsolidadoContribuyenteResponse> contribuyente = null;

                try
                {

                    _logger.Informativo($"Obteniendo contribuyente consolidado...");

                    contribuyente = await ObtenerContribuyenteConsolidadoApi(dto);

                    if (contribuyente is null || contribuyente.Count ==0)
                    {
                        codigoEvento = SriEventos.CONSOLIDAD_CONTRIBUYENTE_NO_OBTENIDO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Contribuyente consolidado obtenida");

                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerContribuyenteConsolidado => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerContribuyenteConsolidado => {exc}");
                        codigoEvento = SriEventos.OBTENER_CONSOLIDAD_CONTRIBUYENTE_ERROR;
                    }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = contribuyente
            };
        }

        public async Task<Respuesta> ObtenerInformacionPlaca(ObtenerInformacionPlacaDto dto)
        {
            string codigoEvento = SriEventos.OBTENER_INFORMACION_PLACA;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
            ObtenerPlacaResponse contribuyente = null;

            try
            {

                _logger.Informativo($"Obteniendo informacion placa...");

                contribuyente = await ObtenerInformacionPlacaApi(dto);

                if (contribuyente is null)
                {
                    codigoEvento = SriEventos.INFORMACION_PLACA_NO_OBTENIDO;
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                    throw new ExcepcionOperativa(codigoEvento);
                }

                _logger.Informativo($"Informacion de placa obtenida");

            }
            catch (Exception exc)
            {
                codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                if (exc is ExcepcionOperativa excOperativa)
                {
                    _logger.Error(
                        $"ObtenerInformacionPlaca => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                    codigoEvento = excOperativa.codigoEvento;
                }
                else
                {
                    _logger.Error($"ObtenerInformacionPlaca => {exc}");
                    codigoEvento = SriEventos.OBTENER_INFORMACION_PLACA_ERROR;
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = contribuyente
            };
        }

        private async Task<IList<ConsolidadoContribuyenteResponse>> ObtenerContribuyenteConsolidadoApi(ObtenerContribuyenteConsolidadoDto dto)
        {
            try
            {
                String urlConsolidadoContribuyente = @"https://srienlinea.sri.gob.ec/sri-catastro-sujeto-servicio-internet/rest/ConsolidadoContribuyente/obtenerPorNumerosRuc?&ruc=" + dto.numeroRuc;

                RespuestaHttp autorizacion = ObtenerTokenApi();

                RespuestaHttp datosRuc = request(urlConsolidadoContribuyente, "", "GET", autorizacion.Data, autorizacion.Cookies);

                return JsonConvert.DeserializeObject<List<ConsolidadoContribuyenteResponse>>(datosRuc.Data);
            }
            catch (Exception exc)
            {
                throw new ExcepcionOperativa(SriEventos.OBTENER_CONSOLIDAD_CONTRIBUYENTE_ERROR, exc);
            }
        }

        private async Task<ObtenerPlacaResponse> ObtenerInformacionPlacaApi(ObtenerInformacionPlacaDto dto)
        {
            try
            {
                String urlConsolidadoContribuyente = @"https://srienlinea.sri.gob.ec/sri-matriculacion-vehicular-recaudacion-servicio-internet/rest/BaseVehiculo/obtenerPorNumeroPlacaOPorNumeroCampvOPorNumeroCpn?numeroPlacaCampvCpn=" + dto.codigoPlaca;

                RespuestaHttp autorizacion = ObtenerTokenApi();

                RespuestaHttp datosRuc = request(urlConsolidadoContribuyente, "", "GET", autorizacion.Data, autorizacion.Cookies);

                return JsonConvert.DeserializeObject<ObtenerPlacaResponse> (datosRuc.Data);
            }
            catch (Exception exc)
            {
                throw new ExcepcionOperativa(SriEventos.OBTENER_INFORMACION_PLACA_ERROR, exc);
            }
        }

        private RespuestaHttp ObtenerTokenApi()
        {
            RespuestaHttp respGenerarCaptcha = request(@"https://srienlinea.sri.gob.ec/sri-captcha-servicio-internet/captcha/start/1?r=123232https://srienlinea.sri.gob.ec/sri-captcha-servicio-internet/rest/ValidacionCaptcha/validarCaptcha/CAPTCHA/?emitirToken=true", "", "GET", "");

            GenerarCaptchaResponse generarCaptchaObject = JsonConvert.DeserializeObject<GenerarCaptchaResponse>(respGenerarCaptcha.Data);

            String generarTokenStr = @"https://srienlinea.sri.gob.ec/sri-captcha-servicio-internet/rest/ValidacionCaptcha/validarCaptcha/" + generarCaptchaObject.values[0] + @"/?emitirToken=true";

            RespuestaHttp respGenerarToken = request(generarTokenStr, "", "GET", "", respGenerarCaptcha.Cookies);

            GenerarTokenResponse generarTokenObject = JsonConvert.DeserializeObject<GenerarTokenResponse>(respGenerarToken.Data);
            RespuestaHttp autorizacion = new RespuestaHttp();
            autorizacion.Data = generarTokenObject.mensaje;
            autorizacion.Cookies = respGenerarCaptcha.Cookies;
            return autorizacion;
        }


        private RespuestaHttp request(string url, string jsonBody, string tipo, string token, List<Cookie> cookies = null)
        {

            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url);

            request2.Method = tipo;
            request2.ContentType = "application/json";
            request2.Accept = "application/json";

            if (token.Length > 0)
            {
                request2.Headers.Add("Authorization", token);
            }


            if (cookies != null)
            {
                request2.CookieContainer = new CookieContainer();
                cookies.ForEach(c => {
                    Uri target = new Uri(url);
                    request2.CookieContainer.Add(new Cookie(c.Name, c.Value) { Domain = target.Host });
                });
            }

            RespuestaHttp respuesta = new RespuestaHttp();

            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();

            List<Cookie> respCookies = new List<Cookie>();

            for (int i = 0; i < response2.Headers.Count; i++)
            {
                string header = response2.Headers.GetKey(i);

                if (header.ToLower() == "set-cookie")
                {
                    foreach (string value in response2.Headers.GetValues(i))
                    {
                        string[] cookiesSplit = value.Split(new[] { ',' });

                        foreach (string value2 in cookiesSplit) {

                            string[] cookie = value2.Split(new[] { '=' }, 2);

                            string tmpStr = cookie[1].Split(new[] { ';' }, 2)[0];

                            respCookies.Add(new Cookie(cookie[0].Trim(), tmpStr.Trim()));
                        }
                    }
                }
            }

            respuesta.Cookies = respCookies;

            using (Stream dataStream = response2.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                respuesta.Data = responseFromServer;

                return respuesta;
            }

        }

    }
}
