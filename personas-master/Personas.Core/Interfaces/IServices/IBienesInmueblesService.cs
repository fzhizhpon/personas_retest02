using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.BienesInmuebles;

namespace Personas.Core.Interfaces.IServices
{
    public interface IBienesInmueblesService
    {
        Task<Respuesta> ObtenerBienesInmueblesSinJoin(long codigoPersona);

        Task<Respuesta> ObtenerBienesInmuebles(ObtenerBienesInmueblesDto dto);

        Task<Respuesta> ObtenerBienInmueble(long codigoPersona, long numeroRegistro);

        Task<Respuesta> GuardarBienesInmuebles(GuardarBienesInmueblesDto obj);

        Task<Respuesta> ActualizarBienesInmuebles(ActualizarBienesInmueblesDto obj);

        Task<Respuesta> EliminarBienesInmuebles(EliminarBienesInmueblesDto obj);
    }
}