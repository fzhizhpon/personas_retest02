using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITipoContribuyenteRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectTiposContribuyentes();
    }
}
