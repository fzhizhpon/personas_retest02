using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Entities
{
    public class Parentesco
    {
        public int codigoParentesco { get; set; }
        public string nombre { get; set; }
        public char estado { get; set; }
        public string codigoAlterno { get; set; }
        public int orden { get; set; }
        public int gradoConsanguinidad { get; set; }
        public int gradoAfinidad { get; set; }
    }
}
