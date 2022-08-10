using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IPersonaNoNaturalRepository
    {
        Task<int> GuardarPersonaNoNatural(object dto);
        
        Task<int> ActualizarPersonaNoNatural(ActualizarPersonaNoNaturalDto dto);
        
        Task<PersonaNoNatural> ObtenerPersonaNoNatural(long codigoPersona);
    }
}
