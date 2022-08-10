using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.RelacionInstitucion
{
    class PersonaRelacionInstitucion
    {
        public long codigoRelacion { get; set; }
        public long codigoPersona { get; set; }
        public DateTime fechaAsignacion { get; set; }
        public long usuarioAsigna { get; set; }
        public string codigoAsignado { get; set; }
        public string estado { get; set; }

        public long codigoAgenciaRegistra { get; set; }
    }
}
