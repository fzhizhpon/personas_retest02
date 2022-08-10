using Personas.Core.Entities.Direcciones;
using System;
namespace Personas.Core.Dtos.Direcciones
{
    public class DireccionOutDto
    {
        public Direccion direccion { get; set; }

        public TelefonoFijoOutDto telefonoFijo { get; set; }

    }
}

