using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Personas
{
    public class ActualizarPersonaNaturalDto
    {
        public long codigoPersona { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public char tieneDiscapacidad { get; set; }
        public int? codigoTipoDiscapacidad { get; set; }
        public int? porcentajeDiscapacidad { get; set; }
        public int codigoPaisNacimiento { get; set; }
        public int codigoProvinciaNacimiento { get; set; }
        public int codigoCiudadNacimiento { get; set; }
        public int codigoParroquiaNacimiento { get; set; }
        public int codigoTipoSangre { get; set; }
        public long? codigoConyuge { get; set; }
        public int codigoEstadoCivil { get; set; }
        public int codigoGenero { get; set; }
        public int codigoProfesion { get; set; }
        public int codigoTipoEtnia { get; set; }
        public char esVulnerable { get; set; }


        [JsonIgnore]
        public int codigoUsuarioActualiza { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }
    }
}
