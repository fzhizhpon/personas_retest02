using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.ReferenciasPersonales;
using Personas.Core.Entities.ReferenciasPersonales;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IReferenciasPersonalesRepository
    {

        Task<int> GuardarReferenciaPersonal(GuardarReferenciaPersonalDto dto);
        //Task<(int, int)> ActualizarReferenciaPersonal(ActualizarReferenciaPersonalDto dto);
        Task<int> EliminarReferenciaPersonal(EliminarReferenciaPersonalDto dto);
        Task<ReferenciaPersonal.ReferenciaPersonalJoin> ObtenerReferenciaPersonalJoin(ObtenerReferenciaPersonalDto dto);
        Task<ReferenciaPersonal> ObtenerReferenciaPersonal(ObtenerReferenciaPersonalDto dto);
        Task<IList<ReferenciaPersonal.ReferenciaPersonalMinimo>> ObtenerReferenciasPersonales(ObtenerReferenciasPersonalesDto dto);
        Task GuardarReferenciaPersonalHistorico(ReferenciaPersonal refPersonal);
        Task<int> ObtenerCodigoReferenciaFinanciera(int codigoPersona);
        Task<object> esUnaReferenciaPersonalEliminada(int codigoPersonaReferida, int codigoPersona);
        Task<int> reactivarReferenciaPersonal( ActualizarReferenciaPersonalDto referenciaPersonalDto);

        

    }
}
