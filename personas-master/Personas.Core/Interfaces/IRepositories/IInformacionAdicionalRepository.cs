using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.TablasComunes;
using Personas.Core.Entities.TablasComunes;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IInformacionAdicionalRepository
    {
        Task<(int, IEnumerable<InformacionAdicional>)> ObtenerInformacionAdicional(long codigoReferencia, long codigoTabla);

        Task<int> GuardarInformacionAdicional(GuardarInformacionAdicionalDto obj);
        
        Task<int> ActualizarInformacionAdicional(ActualizarInformacionAdicionalDto obj);

    }
}