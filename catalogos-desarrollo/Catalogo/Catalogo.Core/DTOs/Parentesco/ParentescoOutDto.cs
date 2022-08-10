using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.DTOs.Parentesco
{
    public class ParentescoOutDto
    {
        public int codigoParentesco { get; set; }
        public string nombre { get; set; }
        public int gradoConsanguinidad { get; set; }
        public int gradoAfinidad { get; set; }
    }
}
