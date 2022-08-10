using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.BienesIntangibles;
using Personas.Core.Entities.BienesIntangibles;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IBienesIntangiblesRepository
    {
        Task<long> ObtenerNumeroRegistroMax();

        Task<(int, IEnumerable<BienesIntangibles>)> ObtenerBienesIntangibles(long codigoPersona);

        Task<(int, BienesIntangibles)> ObtenerBienIntangible(long codigoPersona, long numeroRegistro);

        Task<int> GuardarBienesIntangibles(GuardarBienesIntangiblesDto obj);

        Task<int> ActualizarBienesIntangibles(ActualizarBienesIntangiblesDto obj);

        Task<int> EliminarBienesIntangibles(EliminarBienesIntangiblesDto obj);
    }
}