using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITablasComunesDetallesReposity
    {
        Task<(int, IEnumerable<ComboDto>)> ObtenerTablasComunesDetalles(ObtenerTablasComunesDetallesDto dto);
    }
}