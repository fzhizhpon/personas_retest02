using Catalogo.Core.Interfaces.IRepositories;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class MensajeRespuestaRepository : IMensajeRespuestaRepository
    {
        public async Task<string> ObtenerTextoInfo(string idioma, int codigoEvento, string nombreModulo)
        {
            return "Mensaje prueba";
        }
    }
}
