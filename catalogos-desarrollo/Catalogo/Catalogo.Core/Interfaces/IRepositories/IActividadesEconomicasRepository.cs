using Catalogo.Core.DTOs;
using Catalogo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IActividadesEconomicasRepository
    {
        public Task<(int, IEnumerable<ComboStringDto>)> ObtenerActividadesEconomicas(ActividadComercialDto dto);

    }
}
