using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vimasistem.QueryFilter;
using Vimasistem.QueryFilter.Attributes;

namespace Catalogo.Core.DTOs
{
    public class ActividadComercialDto : QueryFilter
    {

        [Filter("DESCRIPCION", "p")]
        public string descripcion { get; set; }
        [Filter("CODIGO", "p")]
        public string codigo { get; set; }
        public int? indiceInicial { get; set; }
        public int? numeroRegistros { get; set; }

    }
}
