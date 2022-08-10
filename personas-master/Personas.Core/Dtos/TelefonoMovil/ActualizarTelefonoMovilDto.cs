namespace Personas.Core.Dtos.TelefonoMovil
{
    public class ActualizarTelefonoMovilDto
    {
        public long codigoTelefonoMovil { get; set; }

        public long codigoPersona { get; set; }

        public int codigoOperadora { get; set; }

        public string observaciones { get; set; }

        public char principal { get; set; }
    }
}
