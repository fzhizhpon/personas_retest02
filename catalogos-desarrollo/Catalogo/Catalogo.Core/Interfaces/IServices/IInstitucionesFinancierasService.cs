using Catalogo.Core.DTOs;
using System.Threading.Tasks;


namespace Catalogo.Core.Interfaces.IServices
{
    public interface IInstitucionesFinancierasService
    {
        Task<Respuesta> ObtenerInstitucionesFinancieras(ObtenerInstitucionFinancieraDto dto);

        Task<Respuesta> ObtenerInsitucionesFinancierasFull();
    }
}