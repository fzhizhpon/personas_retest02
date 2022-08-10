using Personas.Core.Dtos.Representantes;
using Personas.Core.Entities.Representantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IRepresentantesRepository
    {
        Task<int> GuardarRepresentante(GuardarRepresentanteDto dto);
        Task<int> ActualizarRepresentante(ActualizarRepresentanteDto dto);
        Task<int> EliminarRepresentante(EliminarRepresentanteDto dto);
        Task<Representante.RepresentanteJoin> ObtenerRepresentante(ObtenerRepresentanteDto dto);
        Task<IList<Representante.RepresentanteJoin>> ObtenerRepresentantes(ObtenerRepresentantesDto dto);
        Task<IList<Representante.RepresentanteSimple>> ObtenerRepresentantesPrincipales(ObtenerRepresentantesDto dto);
        Task<Representante.RepresentanteSimple> ObtenerRepresentanteMinimo(ObtenerRepresentanteDto dto);
        Task<int> ActualizarRepresentantesPrincipales(IList<Representante.RepresentanteSimple> dto);
        Task GuardarRepresentanteHistorico(Representante Representante);

        Task<object> obtenerRepresentanteCodigoPersonaCodigoRepre(int codigoPersona, int codigoRepresentante);

        Task<object> esUnRepresetanteEliminado(int codigoPersona, int codigoRepresentante);

        Task<int> reactivarRepresentante(object representante);

        List<Representante.RepresentanteJoin> ObtenerRepresentantesFiltros(
            RepresentanteRequest representanteRequest);
    }
}