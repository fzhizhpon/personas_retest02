using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Familiares
{
    public class EliminarFamiliarDto
    {
        public int codigoPersonaFamiliar { get; set; }
        public int codigoPersona { get; set; }
        public char estado { get; } = '1';
        [JsonIgnore] public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore] public DateTime fechaUsuarioActualiza { get; set; }
    }
}
