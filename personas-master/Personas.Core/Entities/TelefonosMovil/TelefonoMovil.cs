namespace Personas.Core.Entities.TelefonosMovil
{
    public class TelefonoMovil
    {
        public long codigoTelefonoMovil { get; set; }

        public long codigoPersona { get; set; }

        public int codigoPais { get; set; }
        public string descripcion { get; set; }

        public string numero { get; set; }

        public int codigoOperadora { get; set; }

        public string observaciones { get; set; }

        public char principal { get; set; }
    }
}