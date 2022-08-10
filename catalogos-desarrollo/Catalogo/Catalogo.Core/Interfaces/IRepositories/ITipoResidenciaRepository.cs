using Catalogo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ITipoResidenciaRepository
    {
        Task<(int, IEnumerable<ComboDto>)> SelectTiposResidencias();
    }
}
