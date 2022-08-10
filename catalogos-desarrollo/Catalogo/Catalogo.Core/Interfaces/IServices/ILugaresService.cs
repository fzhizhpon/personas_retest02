using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Lugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ILugaresService
    {
        public Task<Respuesta> ObtenerLugares(ObtenerLugaresDto dto);
    }
}
