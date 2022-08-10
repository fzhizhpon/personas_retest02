namespace Personas.Core.Dtos.CorreosElectronicos
{
    public class AgregarCorreoElectronicoDto : AuditDto
    {
        public int codigoPersona { get; set; }
        public string correoElectronico { get; set; }
        public char esPrincipal { get; set; }
        public string observaciones { get; set; }
    }
}