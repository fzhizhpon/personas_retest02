using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Direcciones
{
    public class EliminarDireccionDto
    {
        public int numeroRegistro { get; set; }
        public int codigoPersona { get; set; }
        [JsonIgnore] public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore] public DateTime fechaUsuarioActualiza { get; set; }
    }
}