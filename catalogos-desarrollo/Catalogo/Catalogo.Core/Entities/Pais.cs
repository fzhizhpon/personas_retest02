namespace Catalogo.Core.Entities
{
    public class Pais
    {
        public int codigo { get; set; }

        public string descripcion { get; set; }

        public string isoAlpha2 { get; set; }
        
        public string isoAlpha3 { get; set; }
        
        public string isoNumerico { get; set; }

        public char estado { get; set; }

        public string codigoAlterno { get; set; }
        
        public string codigoArea { get; set; }
    }   
}
