using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface INivelInstruccionService
    {
        Task<Respuesta> ObtenerNivelesInstruccion();
    }
}
