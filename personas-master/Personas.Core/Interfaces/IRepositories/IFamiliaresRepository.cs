using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.Familiares;
using Personas.Core.Entities.Familiares;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IFamiliaresRepository
    {
        Task<int> GuardarFamiliar(GuardarFamiliarDto dto);
        Task<int> ActualizarFamiliar(ActualizarFamiliarDto dto);
        Task<int> EliminarFamiliar(EliminarFamiliarDto dto);
        Task<IList<Familiar.FamiliarJoinMinimo>> ObtenerFamiliaresJoinMinimo(ObtenerFamiliaresDto dto);
        Task<Familiar.FamiliarJoinFull> ObtenerFamiliarJoinFull(ObtenerFamiliarDto dto);
        Task GuardarFamiliarHistorico(Familiar Familiar);
        Task<Familiar> ObtenerFamiliar(ObtenerFamiliarDto dto);
    }

}
