namespace Personas.Core.Dtos.BienesIntangibles
{
    public class EliminarBienesIntangiblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
    }
}