using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Entities.Personas
{
    public class Persona
    {
        public long codigoPersona { get; set; }

        public string numeroIdentificacion { get; set; }

        [JsonIgnore]
        public DateTime fechaRegistro { get; set; }

        public string observaciones { get; set; }

        public int codigoTipoIdentificacion { get; set; }

        public int codigoTipoPersona { get; set; }

        [JsonIgnore]
        public long codigoUsuarioRegistra { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioRegistra { get; set; }

        public int codigoTipoContribuyente { get; set; }

        public int codigoAgencia { get; set; }

        public string codigoDocumento { get; set; }

        [JsonIgnore]
        public string guid { get; set; }

        public class PersonaJoinMinimo
        {
            public long codigoPersona { get; set; }
            public int codigoTipoPersona { get; set; }
            public string nombre { get; set; }
        }
    }

}
