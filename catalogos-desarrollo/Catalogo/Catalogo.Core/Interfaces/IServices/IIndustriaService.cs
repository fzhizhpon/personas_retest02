using Catalogo.Core.DTOs;
using Catalogo.Core.Entities;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IIndustriaService
    {
        Task<Respuesta> ObtenerIndustriaPorSubSectorEconomico(ObtenerIndustriaDto dto);
    }
}
