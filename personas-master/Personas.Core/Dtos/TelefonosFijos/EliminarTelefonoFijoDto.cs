using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.TelefonosFijos
{
    public class EliminarTelefonoFijoDto
    {
        public int codigoPersona { get; set; }
        public int numeroRegistro { get; set; }
        public DateTime fechaUsuarioActualiza { get; set; }
        public int codigoUsuarioActualiza { get; set; }

    }
}
