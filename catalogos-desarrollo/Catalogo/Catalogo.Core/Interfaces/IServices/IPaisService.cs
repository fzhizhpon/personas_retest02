using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IPaisService
    {
        Task<Respuesta> ObtenerPaises(PaginacionDto dto);
    }
}
