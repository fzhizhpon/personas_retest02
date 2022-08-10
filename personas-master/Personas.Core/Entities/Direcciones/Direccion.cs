using System;

namespace Personas.Core.Entities.Direcciones
{
    public class Direccion
    {
        public int numeroRegistro { get; set; }
        public int codigoPersona { get; set; }
        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public string numeroCasa { get; set; }
        public string sector { get; set; }
        public string codigoPostal { get; set; }
        public char esMarginal { get; set; }
        public float latitud { get; set; }
        public float longitud { get; set; }
        public char principal { get; set; }
        public int codigoTipoResidencia { get; set; }
        public string comunidad { get; set; }
        public char tipoSector { get; set; }
        public string referencia { get; set; }
        public DateTime fechaInicialResidencia { get; set; }
        public decimal valorArriendo { get; set; }
    }
}