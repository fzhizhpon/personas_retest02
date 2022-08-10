using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.ReferenciasFinancieras
{
    public class ObtenerReferenciasFinancierasDto
    {
        public int codigoPersona { get; set; }
        public PaginacionDto paginacion { get; set; }

    }
}
