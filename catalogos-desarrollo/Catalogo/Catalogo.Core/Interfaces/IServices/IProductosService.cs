using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Producto;
using System.Threading.Tasks;


namespace Catalogo.Core.Interfaces.IServices
{
    public interface IProductosService
    {
        Task<Respuesta> ObtenerProductosActividadFinanciera(ObtenerProductoActividadFinancieraDto dto);
        Task<Respuesta> ObtenerProductosCodigoGrupo(ObtenerProductoCodigoGrupoDto dto);
    }
}