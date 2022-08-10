using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.Familiares
{
    public class ObtenerFamiliarDto
    {
        public int codigoPersonaFamiliar { get; set; }
        public int codigoPersona { get; set; }
        public char estado { get; } = '1';
    }
}
