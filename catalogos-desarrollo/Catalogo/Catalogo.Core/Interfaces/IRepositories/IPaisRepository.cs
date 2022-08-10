using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IPaisRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectPaises();
        
        Task<(int, IEnumerable<ComboDto>)> SelectPaises(PaginacionDto dto);
    }
}
