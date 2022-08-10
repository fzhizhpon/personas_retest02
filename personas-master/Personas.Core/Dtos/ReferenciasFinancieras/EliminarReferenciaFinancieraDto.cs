using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.ReferenciasFinancieras
{
    public class EliminarReferenciaFinancieraDto
    {
        public long codigoPersona { get; set; }
        public long numeroRegistro { get; set; }


        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }
        [JsonIgnore]
        public long codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public string guid { get; set; }

    }
}
