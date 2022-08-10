using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IPersonaNaturalService
    {
        Task<Respuesta> ObtenerInfoPesona(long codigoPersona);
        
        Task<Respuesta> ObtenerPersonaNatural(long codigoPersona);

        Task<Respuesta> GuardarPersonaNatural(GuardarPersonaNaturalDto dto);
        
        Task<Respuesta> ActualizarPersonaNatural(ActualizarPersonaNaturalDto dto);
    }
}
