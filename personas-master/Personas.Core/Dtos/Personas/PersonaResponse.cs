namespace Personas.Core.Dtos.Personas
{
	public class PersonaResponse
	{
        public int codigoPersona { get; set; }
        public int codigoTipoPersona { get; set; }
        public int codigoTipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string descripcionTipoPersona { get; set; }
        public string descripcionTipoIdentificacion { get; set; }
        public string nombre { get; set; }
    }
}

