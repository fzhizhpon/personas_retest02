using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Personas
{
    public class GuardarPersonaNoNaturalDto : GuardarPersonaDto
    {
        [JsonIgnore]
        public long codigoPersona { get; set; }

        public string razonSocial { get; set; }

        public DateTime fechaConstitucion { get; set; }

        public string objetoSocial { get; set; }

        public int tipoSociedad { get; set; }

        public char finalidadLucro { get; set; }

        public char obligadoLlevarContabilidad { get; set; }

        public char agenteRetencion { get; set; }

        public string direccionWeb { get; set; }

        [JsonIgnore]
        public long codigoUsuarioRegistra { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioRegistra { get; set; }
    }
}
