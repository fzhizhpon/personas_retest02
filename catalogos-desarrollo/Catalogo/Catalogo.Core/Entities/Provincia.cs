namespace Catalogo.Core.Entities
{
    public class Provincia
    {
        public int codigoPais { get; set; }
        
        public int codigoProvincia { get; set; }

        public string descripcion { get; set; }
        
        public string prefijoNumTelefono { get; set; }

        public char estado { get; set; } = '1';

        public string codigoAlerno { get; set; }
    }
}
