using Personas.Core.Dtos.ReferenciasFinancieras;
using Personas.Core.Entities.ReferenciasFinancieras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IReferenciasFinancierasRepository
    {
        Task<int> GuardarReferenciaFinanciera(GuardarReferenciaFinancieraDto dto);
        Task<int> EliminarReferenciaFinanciera(EliminarReferenciaFinancieraDto dto);
        Task<int> ActualizarReferenciaFinanciera(ActualizarReferenciaFinancieraDto dto);
        Task<ReferenciaFinanciera> ObtenerReferenciaFinanciera(ObtenerReferenciaFinancieraDto dto);
        Task<IList<ReferenciaFinanciera>> ObtenerReferenciasFinancieras(ObtenerReferenciasFinancierasDto dto);
        Task GuardarReferenciaFinancieraHistorico(ReferenciaFinanciera refFinanciera);
        Task<int> ObtenerCodigoReferenciaFinanciera(long codigoPersona);
    }
}
