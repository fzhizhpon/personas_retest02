using Personas.Core.App;
using Personas.Core.Dtos.Familiares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface IFamiliaresService
    {
        Task<Respuesta> GuardarFamiliar(GuardarFamiliarDto dto);
        Task<Respuesta> ActualizarFamiliar(ActualizarFamiliarDto dto);
        Task<Respuesta> EliminarFamiliar(EliminarFamiliarDto dto);
        Task<Respuesta> ObtenerFamiliar(ObtenerFamiliarDto dto);
        Task<Respuesta> ObtenerFamiliares(ObtenerFamiliaresDto dto);
    }
}
