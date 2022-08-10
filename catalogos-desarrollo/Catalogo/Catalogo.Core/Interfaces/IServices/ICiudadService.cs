using Catalogo.Core.DTOs;
using Catalogo.Core.Entities;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ICiudadService
    {
        Task<Respuesta> ObtenerCiudadesPorProvincia(ObtenerCiudadProvincia dto);
    }
}
