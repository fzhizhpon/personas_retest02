using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Personas
{
    public class ActualizarPersonaDto
    {
        public int codigoPersona { get; set; }

        public string numeroIdentificacion { get; set; }

        public string observaciones { get; set; }

        public int codigoTipoIdentificacion { get; set; }

        public int codigoTipoPersona { get; set; }

        public int codigoTipoContribuyente { get; set; }

        public int codigoAgencia { get; set; }

        [JsonIgnore]
        public int? codigoUsuarioRegistra { get; set; }

        [JsonIgnore]
        public DateTime? fechaUsuarioRegistra { get; set; }
    }
}
