using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IAgenciaRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectAgenciasPorSucursal(ObtenerAgenciaSucursalDto dto);

        Task<(int, IEnumerable<ComboDto>)> SelectAgencias();
    }
}