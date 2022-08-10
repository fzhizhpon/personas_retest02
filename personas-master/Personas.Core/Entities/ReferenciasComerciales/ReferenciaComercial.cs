using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Personas.Core.Entities.ReferenciasComerciales
{
    public class ReferenciaComercial
    {
        public int? numeroRegistro { get; set; }
        public int? codigoPersona { get; set; }
        public int? codigoPais { get; set; }
        public int? codigoProvincia { get; set; }
        public int? codigoCiudad { get; set; }
        public int? codigoParroquia { get; set; }
        public string establecimiento { get; set; }
        public DateTime? fechaRelacion { get; set; }
        public decimal? montoCredito { get; set; }
        public string plazo { get; set; }
        public int? codigoTipoTiempo { get; set; }
        public string telefono { get; set; }

        [JsonIgnore]
        public int? codigoUsuarioRegistro { get; set; }
        [JsonIgnore]
        public DateTime? fechaUsuarioRegistro { get; set; }
        [JsonIgnore]
        public char estado { get; set; }
        [JsonIgnore]
        public string guid { get; set; }

        public class ReferenciaComercialMinimo
        {
            public int numeroRegistro { get; set; }
            public int codigoPersona { get; set; }
            public int codigoPais { get; set; }
            public string establecimiento { get; set; }
            public string telefono { get; set; }
            public DateTime fechaRelacion { get; set; }
        }

    }
}
