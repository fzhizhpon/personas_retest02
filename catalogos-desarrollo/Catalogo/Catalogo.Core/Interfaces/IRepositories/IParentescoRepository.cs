using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Parentesco;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IParentescoRepository
    {
        Task<(int, IEnumerable<ParentescoOutDto>)> SelectParentescos();
    }
}
