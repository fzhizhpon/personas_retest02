namespace Personas.Core.Entities.Accionistas
{
    public class Accionista : Auditoria
    {
        public long codigoPersona { get; set; }

        public long codigoAccionista { get; set; }

        public decimal porcentajeAcciones { get; set; }
    }
}
