using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.TelefonosFijos
{
    public class GuardarTelefonoFijoDto
    {
        public int codigoPersona { get; set; }
        public int codigoDireccion { get; set; }
        public string numero { get; set; }
        public int codigoOperadora { get; set; }
        public string observaciones { get; set; }


        [JsonIgnore]
        public int numeroRegistro { get; set; }
        [JsonIgnore]
        public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }

    }
}
