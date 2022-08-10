using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface IMensajeRespuestaRepository
    {
        Task<string> ObtenerTextoInfo(string idioma, int codigoEvento, string nombreModulo);
    }
}
