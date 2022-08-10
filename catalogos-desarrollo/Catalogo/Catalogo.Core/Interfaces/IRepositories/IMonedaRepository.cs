using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IMonedaRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectMonedas();
    }
}
