using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Familiares
{
    public class GuardarFamiliarDto
    {
        public int codigoPersonaFamiliar { get; set; }
        public int codigoPersona { get; set; }
        public int codigoParentesco { get; set; }
        public char esCargaFamiliar { get; set; }
        public string observacion { get; set; }


        [JsonIgnore]
        public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }
        [JsonIgnore]
        public char estado { get; } = '1';
    }
}
