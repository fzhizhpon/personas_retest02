using Personas.Core.Dtos;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IHistoricosRepository<T>
    {
        Task<bool> GuardarHistorico(HistoricoDto<T> dto);
    }
}
