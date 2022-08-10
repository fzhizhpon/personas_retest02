using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos.CorreosElectronicos;
using Personas.Core.Entities.CorreosElectronicos;

namespace Personas.Core.Interfaces.IServices
{
    public interface ICorreosElectronicosService
	{
		Task<Respuesta> ObtenerCorreos(int codigoPersona);

		Task<Respuesta> AgregarCorreo(AgregarCorreoElectronicoDto dto);

		Task<Respuesta> ActualizarCorreo(ActualizarCorreoElectronicoDto dto);

		Task<Respuesta> EliminarCorreo(EliminarCorreoRequest dto);
	}
}

