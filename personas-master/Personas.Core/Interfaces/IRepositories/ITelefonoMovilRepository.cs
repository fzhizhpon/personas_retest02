using Personas.Core.Dtos.TelefonoMovil;
using Personas.Core.Entities.TelefonosMovil;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface ITelefonoMovilRepository
    {
        Task<(int, int)> GuardarTelefonoMovil(GuardarTelefonoMovilDto dto);

        Task<(int, IEnumerable<TelefonoMovil>)> ObtenerTelefonosMovil(ObtenerTelefonosMovilDto dto);

        Task<object> ObtenerTelefonoMovil(ObtenerTelefonoMovilDto dto);

        Task<(int, int)> ActualizarTelefonoMovil(ActualizarTelefonoMovilDto dto);

        Task<(int, int)> EliminarTelefonoMovil(EliminarTelefonoMovilDto dto);

        Task<object> esUnTelefonoMovillEliminado(int codigoPersona, string numero);

        Task<int> reactivarTelefonoMovil(object telefonoMovil);
    }
}