using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Provincia;
using Catalogo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IProvinciaRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectProvinciasPorPais(ObtenerProvinciaPais dto);
        
        Task<(int, Provincia)> SelectProvincia(ObtenerProvinciaDto dto);
    }
}
