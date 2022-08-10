using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ISucursalRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectSucursales();
    }
}
