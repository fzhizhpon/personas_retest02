using Catalogo.Core.DTOs.Lugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ILugaresRepository
    {
        Task<(int, IEnumerable<LugarOutDto>)> SelectLugares(ObtenerLugaresDto dto);
    }
}
