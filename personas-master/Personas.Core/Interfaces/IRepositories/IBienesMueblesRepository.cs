using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.BienesMuebles;
using Personas.Core.Entities.BienesMuebles;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IBienesMueblesRepository
    {
        Task<long> ObtenerNumeroRegistroMax();

        Task<(int, IEnumerable<BienesMuebles>)> ObtenerBienesMuebles(long codigoPersona);
        
        Task<(int, BienesMuebles)> ObtenerBienMueble(long codigoPersona , long numeroRegistro);

        Task<int> GuardarBienesMuebles(GuardarBienesMueblesDto obj);

        Task<int> ActualizarBienesMuebles(ActualizarBienesMueblesDto obj);

        Task<int> EliminarBienesMuebles(EliminarBienesMueblesDto obj);
    }
}