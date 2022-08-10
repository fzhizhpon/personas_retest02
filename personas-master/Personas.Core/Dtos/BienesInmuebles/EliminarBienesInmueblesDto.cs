namespace Personas.Core.Dtos.BienesInmuebles
{
    public class EliminarBienesInmueblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
    }
}