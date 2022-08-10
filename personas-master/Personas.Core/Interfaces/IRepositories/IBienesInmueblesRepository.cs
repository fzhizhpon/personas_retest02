using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.BienesInmuebles;
using Personas.Core.Entities.BienesInmuebles;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IBienesInmueblesRepository
    {
        Task<long> ObtenerNumeroRegistroMax();
        
        Task<(int, IEnumerable<BienesInmuebles.MinimoSinJoin>)> ObtenerBienesInmueblesSinJoin(long codigoPersona);
        
        Task<(int, BienesInmuebles)> ObtenerBienInmueble(long codigoPersona , long numeroRegistro);

        Task<int> GuardarBienesInmuebles(GuardarBienesInmueblesDto obj);

        Task<int> ActualizarBienesInmuebles(ActualizarBienesInmueblesDto obj);

        Task<int> EliminarBienesInmuebles(EliminarBienesInmueblesDto obj);

        Task<(int, IEnumerable<BienesInmuebles.Minimo>)> ObtenerBienesInmuebles(ObtenerBienesInmueblesDto dto);
    }
}