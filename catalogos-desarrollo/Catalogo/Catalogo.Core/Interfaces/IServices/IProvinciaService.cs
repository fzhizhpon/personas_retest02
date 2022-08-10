using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Provincia;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IProvinciaService
    {
        Task<Respuesta> ObtenerProvinciasPorPais(ObtenerProvinciaPais dto);
     
        Task<Respuesta> ObtenerProvincia(ObtenerProvinciaDto dto);
    }
}
