using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-trabajos")]
    [ApiController]
    public class TiposTrabajosController : ControllerBase
    {
        private readonly ITipoTrabajoService _service;

        public TiposTrabajosController(ITipoTrabajoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposTrabajos()
        {
            return Ok(await _service.ObtenerTiposTrabajos());
        }
    }
}
