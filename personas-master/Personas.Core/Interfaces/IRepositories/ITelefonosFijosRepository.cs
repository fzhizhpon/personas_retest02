using Personas.Core.Dtos.TelefonosFijos;
using Personas.Core.Entities.TelefonosFijos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface ITelefonosFijosRepository
    {
        Task<int> GenerarNumeroRegistro(int codigoPersona);
        Task<int> GuardarTelefonoFijo(GuardarTelefonoFijoDto dto);
        Task<int> ActualizarTelefonoFijo(ActualizarTelefonoFijoDto dto);
        Task<int> EliminarTelefonoFijo(EliminarTelefonoFijoDto dto);
        Task<IList<TelefonoFijo.TelefonoFijoMinimo>> ObtenerTelefonosFijos(ObtenerTelefonosFijosDto dto);
        Task<TelefonoFijo.TelefonoFijoFull> ObtenerTelefonoFijo(ObtenerTelefonoFijoDto dto);


    }
}
