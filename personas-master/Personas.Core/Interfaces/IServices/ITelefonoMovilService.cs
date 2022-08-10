using Personas.Core.App;
using Personas.Core.Dtos.TelefonoMovil;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface ITelefonoMovilService
    {
        Task<Respuesta> GuardarTelefonoMovil(GuardarTelefonoMovilDto dto);
        
        Task<Respuesta> ObtenerTelefonosMovil(ObtenerTelefonosMovilDto dto);
        
        Task<Respuesta> ActualizarTelefonoMovil(ActualizarTelefonoMovilDto dto);
        
        Task<Respuesta> EliminarTelefonoMovil(EliminarTelefonoMovilDto dto);
    }
}
