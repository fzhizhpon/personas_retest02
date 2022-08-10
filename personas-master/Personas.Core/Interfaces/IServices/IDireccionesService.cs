using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.Direcciones;

namespace Personas.Core.Interfaces.IServices
{
    public interface IDireccionesService
    {
        Task<Respuesta> GuardarDireccion(GuardarDireccionDto dto);
        Task<Respuesta> EliminarDireccion(EliminarDireccionDto dto);
        Task<Respuesta> ActualizarDireccion(ActualizarDireccionDto dto);
        Task<Respuesta> ObtenerDireccion(ObtenerDireccionDto dto);
        Task<Respuesta> ObtenerDirecciones(ObtenerDireccionesDto dto);
    }
}