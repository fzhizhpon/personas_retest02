namespace Personas.Core.Dtos.BienesMuebles
{
    public class ActualizarBienesMueblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
        public int tipoBienMueble { get; set; }
        public string codigoReferencia { get; set; }
        public string descripcion { get; set; }
        public float avaluoComercial { get; set; }
    }
}