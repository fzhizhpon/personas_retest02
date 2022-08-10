using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITablasComunesCabeceraRepository
    {
        Task<(int, IEnumerable<ComboDto>)> ObtenerTablasComunesCabeceras(ObtenerTablasComunesCabeceraDto dto);
    }
}