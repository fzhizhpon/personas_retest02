using Personas.Core.App;
using Personas.Core.Dtos.TelefonosFijos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface ITelefonosFijosService
    {
        Task<Respuesta> GuardarTelefonoFijo(GuardarTelefonoFijoDto dto);
        Task<Respuesta> ActualizarTelefonoFijo(ActualizarTelefonoFijoDto dto);
        Task<Respuesta> EliminarTelefonoFijo(EliminarTelefonoFijoDto dto);
        Task<Respuesta> ObtenerTelefonosFijos(ObtenerTelefonosFijosDto dto);
        Task<Respuesta> ObtenerTelefonoFijo(ObtenerTelefonoFijoDto dto);
    }
}
