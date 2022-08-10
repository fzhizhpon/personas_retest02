using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ICiudadRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectCiudadesPorProvincia(ObtenerCiudadProvincia dto);
    }
}
