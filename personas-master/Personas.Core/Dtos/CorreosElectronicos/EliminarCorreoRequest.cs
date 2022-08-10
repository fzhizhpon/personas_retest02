namespace Personas.Core.Dtos.CorreosElectronicos
{
    public class EliminarCorreoRequest : AuditDto
    {
        public int codigoPersona { get; set; }
        public int codigoCorreoElectronico { get; set; }
    }
}