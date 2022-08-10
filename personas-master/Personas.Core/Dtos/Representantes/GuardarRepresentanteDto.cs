using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Representantes
{
    public class GuardarRepresentanteDto
    {
        public int codigoPersona { get; set; }
        public int codigoRepresentante { get; set; }
        public int? codigoTipoRepresentante { get; set; }
        public char principal { get; set; }


        [JsonIgnore]
        public char estado { get; } = '1';
        [JsonIgnore]
        public int? codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public DateTime? fechaUsuarioActualiza { get; set; }
    }
}
