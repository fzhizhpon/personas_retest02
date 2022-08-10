using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.ReferenciasComerciales
{
    public class EliminarReferenciaComercialDto
    {
        public int numeroRegistro { get; set; }
        public int codigoPersona { get; set; }



        [JsonIgnore]
        public char estado { get; } = '0';
        [JsonIgnore]
        public int? codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public DateTime? fechaUsuarioActualiza { get; set; }
        [JsonIgnore]
        public string guid { get; set; }

    }
}
