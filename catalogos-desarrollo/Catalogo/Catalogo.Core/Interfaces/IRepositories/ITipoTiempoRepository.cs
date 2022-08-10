using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITipoTiempoRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectTiposTiempo();
    }
}
