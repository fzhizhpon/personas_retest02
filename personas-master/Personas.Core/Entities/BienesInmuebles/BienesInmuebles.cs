using System;

namespace Personas.Core.Entities.BienesInmuebles
{
    public class BienesInmuebles
    {
        public long numeroRegistro { get; set; }
        public int tipoBienInmueble { get; set; }
        public int codigoPais { get; set; }
        public int codigoProvincia { get; set; }
        public int codigoCiudad { get; set; }
        public int codigoParroquia { get; set; }
        public string sector { get; set; }
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public string comunidad { get; set; }

        public string numero { get; set; }
        public string codigoPostal { get; set; }
        public string descripcion { get; set; }
        public char tipoSector { get; set; }
        public char esMarginal { get; set; }
        public float longitud { get; set; }
        public float latitud { get; set; }
        public float avaluoComercial { get; set; }
        public float avaluoCatastral { get; set; }
        public float areaTerreno { get; set; }
        public float areaConstruccion { get; set; }
        public float valorTerrenoMetrosCuadrados { get; set; }
        public DateTime fechaConstruccion { get; set; }
        public string referencia { get; set; }

        public class Minimo
        {
            public int codigoPersona { get; set; }
            public long numeroRegistro { get; set; }
            public string tipoBienInmueble { get; set; }
            public string sector { get; set; }
            public string callePrincipal { get; set; }
            public string calleSecundaria { get; set; }
            public string descripcion { get; set; }
        }

        public class MinimoSinJoin
        {
            public string numero { get; set; }
            public int codigoPersona { get; set; }
            public long numeroRegistro { get; set; }
            public int codigoPais { get; set; }
            public int codigoProvincia { get; set; }
            public int codigoCiudad { get; set; }
            public int codigoParroquia { get; set; }
            public string sector { get; set; }
            public string callePrincipal { get; set; }
            public string calleSecundaria { get; set; }
            public string descripcion { get; set; }
        }
    }


}