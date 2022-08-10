using Personas.Core.App;
using Personas.Core.Dtos.Trabajos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IServices
{
    public interface ITrabajosService
    {
        Task<Respuesta> GuardarTrabajo(GuardarTrabajoDto dto);
        Task<Respuesta> ActualizarTrabajo(ActualizarTrabajoDto dto);
        Task<Respuesta> EliminarTrabajo(EliminarTrabajoDto dto);
        Task<Respuesta> ObtenerTrabajo(ObtenerTrabajoDto dto);
        Task<Respuesta> ObtenerTrabajos(ObtenerTrabajosDto dto);

    }
}
