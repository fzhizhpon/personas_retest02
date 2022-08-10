using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/usuario-data-base")]
    [ApiController]
    public class UsuariosDatabaseController : ControllerBase
    {
        private readonly IUsuarioDatabaseService _service;

        public UsuariosDatabaseController(IUsuarioDatabaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerUsuarioDatabase()
        {
            return Ok(await _service.ObtenerUsuarioDatabase());
        }
    }
}
