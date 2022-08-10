using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITiposBienesInmueblesRepository
    {
        Task<(int, IEnumerable<ComboDto>)> ObtenerTiposBienesInmuebles();
    }
}