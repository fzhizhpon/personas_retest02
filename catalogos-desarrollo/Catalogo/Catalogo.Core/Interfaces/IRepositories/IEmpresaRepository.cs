using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IEmpresaRepository
    {
        Task<(int, ComboDto)> SelectEmpresa();
    }
}
