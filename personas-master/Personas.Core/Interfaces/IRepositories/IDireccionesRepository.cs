using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.Direcciones;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IDireccionesRepository
    {
        Task<int> GuardarDireccion(GuardarDireccionDto dto);

        Task<int> ObtenerCodigoNuevaDireccion(int codigoPersona);

        Task<int> DesmarcarDireccionPrincipal(int codigoPersona);

        Task<int> NroDireccionesPrincipales(int codigoPersona);

        Task<int> EliminarDireccion(EliminarDireccionDto dto);

        Task<List<DireccionMinResponse>> ObtenerDirecciones(ObtenerDireccionesDto dto);

        Task<DireccionOutDto> ObtenerDireccion(ObtenerDireccionDto dto);

        Task<int> ActualizarDireccion(ActualizarDireccionDto dto);
    }
}
