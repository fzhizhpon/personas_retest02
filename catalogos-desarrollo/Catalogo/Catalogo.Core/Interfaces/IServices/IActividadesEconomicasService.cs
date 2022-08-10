using Catalogo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface IActividadesEconomicasService
    {
        public Task<Respuesta> ObtenerActividadesEconomicas(ActividadComercialDto dto);

    }
}
