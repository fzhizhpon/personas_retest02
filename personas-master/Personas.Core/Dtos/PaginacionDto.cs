using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos
{
    public class PaginacionDto
    {
        public int? indiceInicial { get; set; }
        public int? numeroRegistros { get; set; }

    }
}
