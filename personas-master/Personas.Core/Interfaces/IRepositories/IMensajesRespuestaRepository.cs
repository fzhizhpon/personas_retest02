using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IMensajesRespuestaRepository
    {
        Task<string> ObtenerTextoInfo(string idioma, string codigoEvento, string modulo);

    }
}
