using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITipoAutenticacionRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectTipoAutenticacion();
    }
}
