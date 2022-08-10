using System;

namespace Personas.Core.Dtos.Trabajos
{
    public class EliminarTrabajoDto
    {
        public int codigoTrabajo { get; set; }
        public int codigoPersona { get; set; }
        public int? codigoUsuarioActualiza { get; set; }
        public DateTime? fechaUsuarioActualiza { get; set; }
        public string guid { get; set; }
    }
}
