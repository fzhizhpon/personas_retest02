using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.EstadosFinancieros;
using Personas.Core.Entities.EstadosFinancieros;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IEstadosFinancierosRepository
    {
        Task<int> GuardarEstadosFinancieros(GuardarEstadoFinancieroDto dto);
        Task<int> ActualizarEstadosFinancieros(ActualizarEstadoFinancieroDto dto);
        Task<List<EstadoFinanciero>> ObtenerCuentasEstadosFinancieros(ObtenerCuentasEstadoFinancieroDto dto);
        Task<double> ObtenerValorCuentaPorQuery(string query, int codigoPersona);
    }
}
