using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Profesion;
using Catalogo.Core.Entities;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IProfesionService
    {
        Task<Respuesta> ObtenerProfesiones();
        
        Task<Respuesta> ObtenerProfesion(int codigoProfesion);
        
        Task<Respuesta> GuardarProfesion(Profesion profesion);
        
        Task<Respuesta> EliminarProfesion(int codigoProfesion);
        
        Task<Respuesta> ActualizarProfesion(ActualizarProfesionDto profesion);
    }
}
