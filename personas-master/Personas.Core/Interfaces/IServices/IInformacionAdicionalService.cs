using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.TablasComunes;
using Personas.Core.Entities.TablasComunes;

namespace Personas.Core.Interfaces.IServices
{
    public interface IInformacionAdicionalService
    {
        Task<Respuesta> ObtenerInformacionAdicional(long codigoReferencia, long codigoTabla);

        Task<Respuesta> GuardarInformacionAdicional(GuardarInformacionAdicionalDto obj);

        Task<Respuesta> ActualizarInformacionAdicional(ActualizarInformacionAdicionalDto obj);
    }
}