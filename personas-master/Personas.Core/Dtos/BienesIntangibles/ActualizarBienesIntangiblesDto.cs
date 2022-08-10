namespace Personas.Core.Dtos.BienesIntangibles
{
    public class ActualizarBienesIntangiblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }
        public string descripcion { get; set; }
        public float avaluoComercial { get; set; }
    }
}