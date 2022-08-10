using System;

namespace Personas.Core.Dtos.TelefonosFijos
{
    public class ActualizarTelefonoFijoDto
    {
        public int codigoPersona { get; set; }
        public int codigoDireccion { get; set; }
        public string numero { get; set; }
        public int codigoOperadora { get; set; }
        public string observaciones { get; set; }
        public int numeroRegistro { get; set; }
        public int codigoUsuarioActualiza { get; set; }
        public DateTime fechaUsuarioActualiza { get; set; }
    }
}
