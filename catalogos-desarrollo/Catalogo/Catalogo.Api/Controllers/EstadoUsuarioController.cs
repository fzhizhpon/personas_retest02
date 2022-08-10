using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/estado-usuario")]
    [ApiController]
    public class EstadoUsuarioController : ControllerBase
    {
        private readonly IEstadoUsuarioService _service;

        public EstadoUsuarioController(IEstadoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposTrabajos()
        {
            return Ok(await _service.ObtenerEstadoUsuario());
        }
    }
}
