using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.RelacionInstitucion
{
    class RelacionInstitucion
    {

        public long codigoPersona { get; set; }
        public long codigoRelacion { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaAsignacion { get; set; }
        public long codigoAgenciaRegistra { get; set; }
    }
}
