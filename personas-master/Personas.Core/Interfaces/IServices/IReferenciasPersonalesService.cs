using Personas.Core.App;
using Personas.Core.Dtos.ReferenciasPersonales;
using Personas.Core.Entities.ReferenciasPersonales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IReferenciasPersonalesService
    {

        Task<Respuesta> GuardarReferenciaPersonal(GuardarReferenciaPersonalDto dto);
        //Task<Respuesta> ActualizarReferenciaPersonal(ActualizarReferenciaPersonalDto dto);
        Task<Respuesta> EliminarReferenciaPersonal(EliminarReferenciaPersonalDto dto);
        Task<Respuesta> ObtenerReferenciaPersonal(ObtenerReferenciaPersonalDto dto);
        Task<Respuesta> ObtenerReferenciasPersonales(ObtenerReferenciasPersonalesDto dto);

    }
}
