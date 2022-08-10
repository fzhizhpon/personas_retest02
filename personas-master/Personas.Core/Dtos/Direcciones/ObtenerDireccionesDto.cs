using System;
using Vimasistem.QueryFilter.Attributes;

namespace Personas.Core.Dtos.Direcciones
{
    public class ObtenerDireccionesDto : PaginacionQueryDto
    {
        public int codigoPersona { get; set; }
        
        [Filter("NUMERO_REGISTRO")]
        public int? numeroRegistro { get; set; }
        
        [Filter("CODIGO_PAIS")]
        public int? codigoPais { get; set; }
        
        [Filter("CODIGO_PROVINCIA")]
        public int? codigoProvincia { get; set; }
        
        [Filter("CODIGO_CIUDAD")]
        public int? codigoCiudad { get; set; }
        
        [Filter("CODIGO_PARROQUIA")]
        public int? codigoParroquia { get; set; }
        
        [Filter("CALLE_PRINCIPAL")]
        public string callePrincipal { get; set; }
        
        [Filter("CALLE_SECUNDARIA")]
        public string calleSecundaria { get; set; }
        
        [Filter("NUMERO_CASA")]
        public string numeroCasa { get; set; }
        
        [Filter("SECTOR")]
        public string sector { get; set; }
        
        [Filter("CODIGO_POSTAL")]
        public string codigoPostal { get; set; }
        
        [Filter("ES_MARGINAL")]
        public char? esMarginal { get; set; }
        
        [Filter("LATITUD")]
        public decimal? latitud { get; set; }
        
        [Filter("LONGITUD")]
        public decimal? longitud { get; set; }
        
        [Filter("FECHA_INGRESO")]
        public DateTime? fechaIngreso { get; set; }
        
        [Filter("PRINCIPAL")]
        public char? principal { get; set; }

        [Filter("FECHA_INICIAL_RESIDENCIA")]
        public DateTime fechaInicialResidencia { get; set; }

        [Filter("VALOR_ARRIENDO")]
        public decimal valorArriendo { get; set; }
    }
}