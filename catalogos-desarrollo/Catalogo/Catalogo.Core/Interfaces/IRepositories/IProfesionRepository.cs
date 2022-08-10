using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Profesion;
using Catalogo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IProfesionRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectProfesiones();
        
        Task<(int, Profesion)> SelectProfesion(int codigoProfesion);

        Task<(int, int)> InsertProfesion(Profesion profesion);
        
        Task<(int, int)> DeleteProfesion(int codigoProfesion);
        
        Task<(int, int)> UpdateProfesion(ActualizarProfesionDto profesion);
    }
}
