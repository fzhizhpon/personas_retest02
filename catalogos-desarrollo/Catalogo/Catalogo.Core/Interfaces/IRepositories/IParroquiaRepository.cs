using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IParroquiaRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectParroquiasPorCiudad(ObtenerParroquiaCiudad dto);
    }
}
