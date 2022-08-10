using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.Sri
{

    public class RespuestaHttp
    {
        public string Data { get; set; }

        public List<Cookie> Cookies { get; set; }

    }

    public class GenerarCaptchaResponse 
    { 
        public string imageName { get; set; }
        public string Computadora { get; set; }
        public string imageFieldName { get; set; }
        public List<string> values { get; set; }
        public string audioFieldName { get; set; }
    }

    public class GenerarTokenResponse
    {
        public string mensaje { get; set; }
    }

    public class ConsolidadoContribuyenteResponse
    {
        public string ContribuyenteFantasma { get; set; }
        public object NumeroRucFantasma { get; set; }
        public string NumeroRuc { get; set; }
        public string RazonSocial { get; set; }
        public object NombreComercial { get; set; }
        public object EstadoPersonaNatural { get; set; }
        public string EstadoSociedad { get; set; }
        public string ClaseContribuyente { get; set; }
        public string Obligado { get; set; }
        public string ActividadContribuyente { get; set; }
        public InformacionFechasContribuyente InformacionFechasContribuyente { get; set; }
        public string RepresentanteLegal { get; set; }
        public string AgenteRepresentante { get; set; }
        public string PersonaSociedad { get; set; }
        public string SubtipoContribuyente { get; set; }
        public object MotivoCancelacion { get; set; }
        public object MotivoSuspension { get; set; }
    }

    public class InformacionFechasContribuyente
    {
        public string FechaInicioActividades { get; set; }
        public object FechaCese { get; set; }
        public object FechaReinicioActividades { get; set; }
        public string FechaActualizacion { get; set; }
    }

    public class ObtenerPlacaResponse
    {
        public long CodigoVehiculo { get; set; }
        public string NumeroPlaca { get; set; }
        public object NumeroCamvCpn { get; set; }
        public string ColorVehiculo1 { get; set; }
        public string ColorVehiculo2 { get; set; }
        public long Cilindraje { get; set; }
        public string NombreClase { get; set; }
        public string DescripcionMarca { get; set; }
        public string DescripcionModelo { get; set; }
        public long AnioAuto { get; set; }
        public string DescripcionPais { get; set; }
        public object MensajeMotivoAuto { get; set; }
        public bool AplicaCuota { get; set; }
        public DateTimeOffset FechaUltimaMatricula { get; set; }
        public DateTimeOffset FechaCaducidadMatricula { get; set; }
        public DateTimeOffset FechaCompraRegistro { get; set; }
        public DateTimeOffset FechaRevision { get; set; }
        public string DescripcionCanton { get; set; }
        public string DescripcionServicio { get; set; }
        public long UltimoAnioPagado { get; set; }
        public string ProhibidoEnajenar { get; set; }
        public object Observacion { get; set; }
        public string EstadoExoneracion { get; set; }
    }

}
