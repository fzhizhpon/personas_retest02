using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.BienesMuebles;

namespace Personas.Core.Interfaces.IServices
{
    public interface IBienesMueblesService
    {
        Task<Respuesta> ObtenerBienesMuebles(long codigoPersona);

        Task<Respuesta> ObtenerBienMueble(long codigoPersona , long numeroRegistro);

        Task<Respuesta> GuardarBienesMuebles(GuardarBienesMueblesDto obj);

        Task<Respuesta> ActualizarBienesMuebles(ActualizarBienesMueblesDto obj);

        Task<Respuesta> EliminarBienesMuebles(EliminarBienesMueblesDto obj);
    }
}