namespace Catalogo.Core.Entities
{
    public class Profesion
    {
        public int codigo { get; set; }

        public string nombre { get; set; }

        public string observacion { get; set; }

        public char estado { get; set; } = '1';

        public string codigoAlterno { get; set; }
    }
}
