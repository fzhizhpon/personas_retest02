using System.Collections.Generic;
using System;

namespace Personas.Core.Dtos.Direcciones
{
    public class DireccionMinResponse
    {
        public int numeroRegistro { get; set; }
        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public string numeroCasa { get; set; }
        public string sector { get; set; }
        public char principal { get; set; }
        public float? longitud { get; set; }
        public float? latitud { get; set; }
        public int codigoTipoResidencia { get; set; }
        public string descripcionTipoResidencia { get; set; }
        public string numeroTelFijo { get; set; }
        public int numeroRegistroTelFijo { get; set; }        
        public DateTime fechaInicialResidencia { get; set; }
        public decimal valorArriendo { get; set; }
    }
}

