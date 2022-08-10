using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.BienesIntangibles;

namespace Personas.Core.Interfaces.IServices
{
    public interface IBienesIntangiblesService
    {
        Task<Respuesta> ObtenerBienesIntangibles(long codigoPersona);

        Task<Respuesta> ObtenerBienIntangible(long codigoPersona, long numeroRegistro);

        Task<Respuesta> GuardarBienesIntangibles(GuardarBienesIntangiblesDto obj);

        Task<Respuesta> ActualizarBienesIntangibles(ActualizarBienesIntangiblesDto obj);

        Task<Respuesta> EliminarBienesIntangibles(EliminarBienesIntangiblesDto obj);
    }
}