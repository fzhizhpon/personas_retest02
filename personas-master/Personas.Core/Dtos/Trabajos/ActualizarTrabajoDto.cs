using System;

namespace Personas.Core.Dtos.Trabajos
{
    public class ActualizarTrabajoDto : AuditDto
    {
        public int codigoTrabajo { get; set; }
        public int codigoPersona { get; set; }
        public int codigoCategoria { get; set; }

        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }

        public decimal ingresosMensuales { get; set; }

        public int codigoTipoTiempo { get; set; }
        public int tiempoActividad { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string cargo { get; set; }
        public string codigoActividad { get; set; }
        public char principal { get; set; }
    }
}
