using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ITipoSociedadService
    {
        Task<Respuesta> ObtenerTiposSociedades();
    }
}
