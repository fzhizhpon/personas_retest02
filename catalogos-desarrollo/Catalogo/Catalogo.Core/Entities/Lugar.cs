namespace Catalogo.Core.Entities
{
    public class Lugar
    {
        public int codigoPais { get; set; }
        public string pais { get; set; }
        public char estado { get; set; }
        public string nacionalidad { get; set; }
        public int codigoProvincia { get; set; }
        public string provincia { get; set; }
        public int codigoCiudad { get; set; }
        public string ciudad { get; set; }
        public int codigoParroquia { get; set; }
        public string parroquia { get; set; }
        public string codigoLugar { get; set; }
        public string descripcionLugar { get; set; }
    }
}
