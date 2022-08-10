using System;

namespace Personas.Core.Dtos.BienesInmuebles
{
    public class ActualizarBienesInmueblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
        public int tipoBienInmueble { get; set; }
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public float avaluoComercial { get; set; }
        public float avaluoCatastral { get; set; }
        public float areaConstruccion { get; set; }
        public float valorTerrenoMetrosCuadrados { get; set; }
        public DateTime fechaConstruccion { get; set; }
        public string referencia { get; set; }
        public string comunidad { get; set; }
        public string descripcion { get; set; }
    }
}