using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Core.Dtos.CorreosElectronicos;
using Personas.Core.Entities.CorreosElectronicos;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface ICorreosElectronicosRepository
	{
		List<CorreoElectronicoDto> ObtenerCorreos(int codigoPersona);

		int ActualizarCorreo(CorreoElectronico correo);

		int DesmarcarCorreoPrincipal(int codigoPersona);

		int NroCorreosPrincipales(int codigoPersona);

		int ObtenerCodigoNuevoCorreo(int codigoPersona);

		int AgregarCorreo(int codigoCorreo, AgregarCorreoElectronicoDto dto);

		int EliminarCorreo(EliminarCorreoRequest dto);

		CorreoElectronico ObtenerCorreo(int codigoPersona, int codigoCorreoElectronico);

		CorreoElectronico ObtenerCorreo(int codigoPersona, string correoElectronico);
	}
}

