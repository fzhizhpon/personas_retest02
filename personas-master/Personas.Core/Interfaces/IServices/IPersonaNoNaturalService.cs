using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IPersonaNoNaturalService
    {
        Task<Respuesta> GuardarPersonaNoNatural(GuardarPersonaNoNaturalDto dto);
        
        Task<Respuesta> ObtenerPersonaNoNatural(long codigoPersona);
        
        Task<Respuesta> ActualizarPersonaNoNatural(ActualizarPersonaNoNaturalDto dto);
    }
}
