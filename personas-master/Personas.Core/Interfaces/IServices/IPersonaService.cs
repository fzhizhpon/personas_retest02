using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IPersonaService
    {
        Task<Respuesta> ActualizarPersona(ActualizarPersonaDto dto);
        
        Task<Respuesta> ObtenerPersona(long codigoPersona);

        Task<Respuesta> ObtenerPersonas(PersonaRequest dto);
        Task<Respuesta> ObtenerPersonaJoinMinimo(UltActPersonaRequest dto);

        
    }
}
