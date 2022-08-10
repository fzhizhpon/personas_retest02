using Vimasistem.QueryFilter;

namespace Catalogo.Core.DTOs
{
    public class PaginacionDto : QueryFilter
    {
        public int? indiceInicial { get; set; }
        
        public int? numeroRegistros { get; set; }
    }
}
