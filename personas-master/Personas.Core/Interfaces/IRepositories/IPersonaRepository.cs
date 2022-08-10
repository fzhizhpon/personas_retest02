using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IPersonaRepository
    {
        Task<int> ActualizarPersona(ActualizarPersonaDto dto);

        Task<Persona> ObtenerPersona(long codigoPersona);

        Task<int> GuardarPersona(object dto);

        Task<long> ObtenerCodigoPersonaMax();

        List<PersonaResponse> ObtenerPersonas(PersonaRequest persona);
        Task<Persona.PersonaJoinMinimo> ObtenerPersonaJoinMinimo(UltActPersonaRequest dto);

        Task ColocarFechaUltimaActualizacion(UltActPersonaRequest dto);

        Task<int> ObtenerPersonasPorIdentificacion(string nroIdentificacion);
    }
}
