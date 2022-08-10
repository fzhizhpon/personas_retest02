using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.MensajesRespuesta
{
    public class MensajesRespuestaRequest
    {
        public int codigo { get; set; }
        public string idioma { get; set; }
        public string nombreServicio { get; set; }

    }
}
