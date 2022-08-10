using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposRepresentantesController : ControllerBase
    {
        private readonly ITipoRepresentanteService _service;

        public TiposRepresentantesController(ITipoRepresentanteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposRepresentantes()
        {
            return Ok(await _service.ObtenerTiposRepresentantes());
        }
    }
}
