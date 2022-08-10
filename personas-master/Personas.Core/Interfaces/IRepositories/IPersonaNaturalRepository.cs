using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IPersonaNaturalRepository
    {
        Task<InfoPersonaNaturalDto> ObtenerInfoPersona(long codigoPersona);
        
        Task<PersonaNatural> ObtenerPersonaNatural(long codigoPersona);

        Task<int> GuardarPersonaNatural(object dto);
        
        Task<int> ActualizarPersonaNatural(ActualizarPersonaNaturalDto dto);

        Task<int> ActualizarConyugue(ActualizarConyugueRequest dto);
    }
}
