using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Representantes
{
    public class EliminarRepresentanteDto
    {
        public int codigoPersona { get; set; }
        public int codigoRepresentante { get; set; }


        [JsonIgnore]
        public char estado { get; } = '0'; // 0 es eliminado logico
        
        [JsonIgnore] public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore] public DateTime fechaUsuarioActualiza { get; set; }

    }
}
