using System;

namespace Personas.Core.Dtos.Representantes
{
    public class ActualizarRepresentanteDto
    {
        public int codigoPersona { get; set; }
        public int codigoRepresentante { get; set; }
        public int? codigoTipoRepresentante { get; set; }
        public char principal { get; set; }
        public char estado { get; } = '1';
        public int? codigoUsuarioActualiza { get; set; }
        public DateTime? fechaUsuarioActualiza { get; set; }
        public string guid { get; set; }
    }
}
