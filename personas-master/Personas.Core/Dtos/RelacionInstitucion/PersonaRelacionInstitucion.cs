using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.RelacionInstitucion
{
    public class PersonaRelacionInstitucion
    {

        public long codigoRelacion { get; set; }
        public long codigoPersona { get; set; }
        public DateTime fechaAsignacion { get; set; } = DateTime.Now;
        public long usuarioAsigna { get; set; }
        public string codigoAsignado { get; set; }
        public string estado { get; set; } = "1";

        public long codigoAgenciaRegistra { get; set; }

    }
}
