using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.Representantes;

namespace Personas.Core.Interfaces.IServices
{
    public interface IRepresentantesService
    {
        Task<Respuesta> GuardarRepresentante(GuardarRepresentanteDto dto);
        Task<Respuesta> ActualizarRepresentante(ActualizarRepresentanteDto dto);
        Task<Respuesta> EliminarRepresentante(EliminarRepresentanteDto dto);
        Task<Respuesta> ObtenerRepresentante(int codigoPersona, int codigoRepresentante);
        Task<Respuesta> ObtenerRepresentantes(int codigoPersona);
        Task<Respuesta> ObtenerRepresentantesFiltros(RepresentanteRequest representanteRequest);
    }
}