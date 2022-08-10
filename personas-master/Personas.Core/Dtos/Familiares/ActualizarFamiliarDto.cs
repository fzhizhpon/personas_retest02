using System;

namespace Personas.Core.Dtos.Familiares
{
    public class ActualizarFamiliarDto
    {
        public int codigoPersona { get; set; }
        public int codigoPersonaFamiliar { get; set; }
        public int codigoParentesco { get; set; }
        public string observacion { get; set; }
        public char esCargaFamiliar { get; set; }
        public int codigoUsuarioActualiza { get; set; }
        public DateTime fechaUsuarioActualiza { get; set; }
        public char estado { get; } = '1';
    }
}
