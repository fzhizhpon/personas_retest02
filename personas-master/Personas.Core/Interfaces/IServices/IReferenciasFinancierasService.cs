using Personas.Core.App;
using Personas.Core.Dtos.ReferenciasFinancieras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IReferenciasFinancierasService
    {
        Task<Respuesta> GuardarReferenciaFinanciera(GuardarReferenciaFinancieraDto dto);
        Task<Respuesta> ActualizarReferenciaFinanciera(ActualizarReferenciaFinancieraDto dto);
        Task<Respuesta> EliminarReferenciaFinanciera(EliminarReferenciaFinancieraDto dto);
        Task<Respuesta> ObtenerReferenciaFinanciera(ObtenerReferenciaFinancieraDto dto);
        Task<Respuesta> ObtenerReferenciasFinancieras(ObtenerReferenciasFinancierasDto dto);
    }
}
