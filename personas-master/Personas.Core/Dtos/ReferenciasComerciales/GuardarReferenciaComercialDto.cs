using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.ReferenciasComerciales
{
    public class GuardarReferenciaComercialDto
    {

        public int codigoPersona { get; set; }
        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }
        public string establecimiento { get; set; }
        public DateTime fechaRelacion { get; set; }
        public decimal? montoCredito { get; set; }
        public int? plazo { get; set; }
        public int? codigoTipoTiempo { get; set; }
        public string telefono { get; set; }



        [JsonIgnore]
        public int numeroRegistro { get; set; }
        [JsonIgnore]
        public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public DateTime? fechaUsuarioActualiza { get; set; }
        [JsonIgnore]
        public string guid { get; set; }
        [JsonIgnore]
        public char estado { get; } = '1';
    }
}
