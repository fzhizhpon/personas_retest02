using System;

namespace Personas.Core.Dtos.ReferenciasPersonales
{
    public class ActualizarReferenciaPersonalDto : AuditDto
    {
        public int codigoPersonaReferida { get; set; }
        public int codigoPersona { get; set; }
        public DateTime fechaConoce { get; set; }
        public string observaciones { get; set; }
    }
}