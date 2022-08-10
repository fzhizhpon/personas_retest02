using Vimasistem.QueryFilter;

namespace Personas.Core.Dtos
{
    public class PaginacionQueryDto : QueryFilter
    {
        public int? indiceInicial { get; set; }
        public int? numeroRegistros { get; set; }
    }
}