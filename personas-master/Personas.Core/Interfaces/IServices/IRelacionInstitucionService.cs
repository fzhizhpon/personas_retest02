using Personas.Core.App;
using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.RelacionInstitucion;

namespace Personas.Core.Interfaces.IServices
{
    public interface IRelacionInstitucionService
    {
       
        Task<Respuesta> GuardarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto);
        Task<Respuesta> ObtenerRelacionInstitucion(RelacionInstitucion dto);
        Task<Respuesta> ActualizarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto);

        Task<Respuesta> ObtenerRelacionInstitucionMin(PersonaRelacionInstitucion dto);


    }
}
