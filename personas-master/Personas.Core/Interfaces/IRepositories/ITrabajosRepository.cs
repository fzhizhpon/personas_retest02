using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.Trabajos;
using Personas.Core.Entities.Trabajos;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface ITrabajosRepository
    {
        Task<int> GuardarTrabajo(int codigoTrabajo, GuardarTrabajoDto dto);
        Task<int> ActualizarTrabajo(ActualizarTrabajoDto dto);
        Task<int> EliminarTrabajo(EliminarTrabajoDto dto);
        Task<Trabajo> ObtenerTrabajo(ObtenerTrabajoDto dto);
        Task<IList<Trabajo.TrabajoMinimo>> ObtenerTrabajos(ObtenerTrabajosDto dto);
        Task<int> ObtenerCodigoTrabajo(int codigoPersona);
        
        Task<object> obtenerTrabajoCodigoPersonaRazonSocial(int codigoPersona, string razonSocial);
        
        Task<object> esUnTrabajoEliminado(int codigoPersona, string razonSocial);
        
        Task<int> reactivarTrabajo(object telefonoMovil);
        
    }
}
