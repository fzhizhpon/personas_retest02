using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IInstitucionesFinancierasRepository
    {
        Task<(int, IEnumerable<ComboDto>)> ObtenerInstitucionesFinancieras(ObtenerInstitucionFinancieraDto dto);

        Task<(int, IEnumerable<ComboDto>)> ObtenerInsitucionesFinancierasFull();
    }
}