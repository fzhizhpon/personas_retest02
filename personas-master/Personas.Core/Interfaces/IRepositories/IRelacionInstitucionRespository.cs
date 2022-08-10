using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.RelacionInstitucion;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IRelacionInstitucionRepository
    {
       
        Task<int> GuardarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto);


        Task<List<RelacionInstitucion>> ObtenerRelacionInstitucion(RelacionInstitucion dto);
       

        Task<int> ActualizarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto);

        Task<PersonaRelacionInstitucion> ObtenerRelacionInstitucionMin(PersonaRelacionInstitucion dto);

    }
}
