using System;
namespace Personas.Core.Entities.CorreosElectronicos
{
	public class CorreoElectronico
	{
        public int codigoCorreoElectronico { get; set; }
        public int codigoPersona { get; set; }
        public string correoElectronico { get; set; }
        // <summary>
        // 1: Principal; 0: Secundario
        // </summary>
        public char esPrincipal { get; set; }
        public string observaciones { get; set; }
        public int codigoUsuarioActualiza { get; set; }
        public DateTime fechaActualiza { get; set; }
        // <summary>
        // 1: Activo; 0: Inactivo o eliminado
        // </summary>
        public char estado { get; set; }
    }
}
