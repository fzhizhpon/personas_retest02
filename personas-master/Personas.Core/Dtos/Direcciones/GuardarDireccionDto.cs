using Personas.Core.Dtos.TelefonosFijos;
using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.Direcciones
{
    public class GuardarDireccionDto
    {
        public int codigoPersona { get; set; }
        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public string numeroCasa { get; set; }
        public string sector { get; set; }
        public string? codigoPostal { get; set; }
        public char esMarginal { get; set; }
        public float? latitud { get; set; }
        public float? longitud { get; set; }
        public char principal { get; set; }
        public int codigoTipoResidencia { get; set; }
        public string comunidad { get; set; }
        public string referencia { get; set; }
        public char tipoSector { get; set; }
        public DateTime? fechaInicialResidencia { get; set; }
        public decimal? valorArriendo { get; set; }
        public GuardarTelefonoFijoDto telefonoFijo { get; set; }

        [JsonIgnore]
        public int numeroRegistro { get; set; }
        [JsonIgnore]
        public long codigoUsuarioRegistra { get; set; }

        [JsonIgnore]
        public DateTime fechaUsuarioRegistra { get; set; }
    }
}