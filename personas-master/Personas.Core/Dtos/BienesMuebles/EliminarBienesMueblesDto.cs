namespace Personas.Core.Dtos.BienesMuebles
{
    public class EliminarBienesMueblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
    }
}