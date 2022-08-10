namespace Personas.Core.Dtos.CorreosElectronicos
{
    public class CorreoElectronicoDto
	{
        public int codigoCorreoElectronico { get; set; }
        public int codigoPersona { get; set; }
        public string correoElectronico { get; set; }
        // <summary>
        // 1: Activo; 0: Inactivo o eliminado
        // </summary>
        public char esPrincipal { get; set; }
        public string observaciones { get; set; }
    }
}

