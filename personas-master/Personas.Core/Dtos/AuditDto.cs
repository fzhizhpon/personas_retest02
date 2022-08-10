using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos
{
    public class AuditDto
	{
        [JsonIgnore]
        public long codigoUsuarioActualiza { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }
    }
}

