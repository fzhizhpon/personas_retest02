using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Entities
{
    public class Auditoria
    {
        [JsonIgnore]
        public long codigoUsuarioRegistra { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioRegistra { get; set; }
    }
}
