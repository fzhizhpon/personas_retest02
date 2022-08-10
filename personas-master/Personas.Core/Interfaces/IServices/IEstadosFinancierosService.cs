using Personas.Core.App;
using Personas.Core.Dtos.EstadosFinancieros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IEstadosFinancierosService
    {
        Task<Respuesta> GuardarEstadosFinancieros(GuardarEstadoFinancieroDto dto);
        Task<Respuesta> ActualizarEstadosFinancieros(ActualizarEstadoFinancieroDto dto);
        Task<Respuesta> ObtenerCuentasEstadosFinancieros(ObtenerCuentasEstadoFinancieroDto dto);
    }
}
