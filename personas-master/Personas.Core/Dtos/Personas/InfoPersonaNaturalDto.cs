using System;

namespace Personas.Core.Dtos.Personas
{
    public class InfoPersonaNaturalDto
    {
        public int codigoPersona { get; set; }

        public string tipoIdentificacion { get; set; }

        public string numeroIdentificacion { get; set; }

        public string nombres { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public string correoElectronico { get; set; }

        public string codigoDocumento { get; set; }

        public DateTime fechaUltimaActualizacion { get; set; }

        public FotoIdentificacion fotoIdentificacion { get; set; }
    }

    public class FotoIdentificacion
    {
        public string frontal { get; set; }
        public string posterior { get; set; }
    }
}
