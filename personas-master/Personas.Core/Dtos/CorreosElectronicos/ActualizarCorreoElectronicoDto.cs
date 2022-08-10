namespace Personas.Core.Dtos.CorreosElectronicos
{
    public class ActualizarCorreoElectronicoDto : AuditDto
	{
		public int codigoPersona { get; set; }
        public int codigoCorreoElectronico { get; set; }
        public string correoElectronico { get; set; }
		public char esPrincipal { get; set; }
		public string observaciones { get; set; }
	}
}

