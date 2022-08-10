using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface INombreCargosRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectNombreCargo();
    }
}
