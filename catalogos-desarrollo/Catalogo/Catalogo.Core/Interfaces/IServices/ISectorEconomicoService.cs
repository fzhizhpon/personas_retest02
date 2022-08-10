using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ISectorEconomicoService
    {
        Task<Respuesta> ObtenerSectoresEconomicos(PaginacionDto dto);
    }
}
