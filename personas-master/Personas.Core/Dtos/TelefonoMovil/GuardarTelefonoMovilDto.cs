namespace Personas.Core.Dtos.TelefonoMovil
{
    public class GuardarTelefonoMovilDto
    {
        public int codigoPersona { get; set; }

        public int codigoPais { get; set; }

        public string numero { get; set; }

        public int codigoOperadora { get; set; }

        public char principal { get; set; } = '1';

        public string observaciones { get; set; }
        public int codigoEstado { get; } = 1;


    }
}
