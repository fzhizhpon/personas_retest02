using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITipoIdentificacionRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectTiposIdentificaciones();
    }
}
