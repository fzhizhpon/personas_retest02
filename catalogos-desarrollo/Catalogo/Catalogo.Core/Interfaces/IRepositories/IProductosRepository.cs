using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Producto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IProductosRepository
    {
        Task<(int, IEnumerable<ComboDto>)> ObtenerProductosActividadFinanciera(ObtenerProductoActividadFinancieraDto dto);

        Task<(int, IEnumerable<ComboDto>)> ObtenerProductosCodigoGrupo(ObtenerProductoCodigoGrupoDto dto);
    }
}