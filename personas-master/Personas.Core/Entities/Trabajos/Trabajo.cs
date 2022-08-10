using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Entities.Trabajos
{
    public class Trabajo
    {
        public int codigoTrabajo { get; set; }
        public int codigoPersona { get; set; }
        public int codigoCategoria { get; set; }
        public int codigoTipoTiempo { get; set; }
        public int tiempoActividad { get; set; }

        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }

        public decimal ingresosMensuales { get; set; }

        public DateTime fechaIngreso { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string cargo { get; set; }
        public char principal { get; set; }
        public string codigoActividad { get; set; }

        [JsonIgnore]
        public int codigoUsuarioRegistro { get; set; }
        [JsonIgnore]
        public DateTime fechaUsuarioRegistro { get; set; }
        [JsonIgnore]
        public string guid { get; set; }

        public class TrabajoMinimo
        {
            public int codigoTrabajo { get; set; }
            public int codigoPersona { get; set; }
            public string razonSocial { get; set; }
            public string cargo { get; set; }
            public DateTime fechaIngreso { get; set; }
            public decimal ingresosMensuales { get; set; }
            public string descripcionCategoria { get; set; }
            public char principal { get; set; }
        }

    }
}
