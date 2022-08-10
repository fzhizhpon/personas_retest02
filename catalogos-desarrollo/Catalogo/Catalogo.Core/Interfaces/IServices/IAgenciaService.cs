using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IAgenciaService
    {
        public Task<Respuesta> ObtenerAgenciasPorSucursal(ObtenerAgenciaSucursalDto dto);

        Task<Respuesta> ObtenerAgencias();
    }
}