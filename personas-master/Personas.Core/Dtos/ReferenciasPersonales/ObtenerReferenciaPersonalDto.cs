using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.ReferenciasPersonales
{
    public class ObtenerReferenciaPersonalDto
    {
        public int? codigoPersona { get; set; }
        public int? codigoPersonaReferida { get; set; }
        public char estado { get; } = '1';

    }
}
