using Personas.Core.App;
using Personas.Core.Dtos.Sri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface ISriService
    {
        Task<Respuesta> ObtenerContribuyenteConsolidado(ObtenerContribuyenteConsolidadoDto dto);
        Task<Respuesta> ObtenerInformacionPlaca(ObtenerInformacionPlacaDto dto);
    }
}
