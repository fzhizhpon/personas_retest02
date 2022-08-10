using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IOperadoraMovilService
    {
        Task<Respuesta> ObtenerOperadorasMoviles();
        Task<Respuesta> ObtenerPaisesMarcadoMoviles();

    }
}
