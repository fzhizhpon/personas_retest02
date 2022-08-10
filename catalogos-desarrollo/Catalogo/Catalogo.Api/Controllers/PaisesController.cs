using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IPaisService _service;

        public PaisesController(IPaisService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerPaises([FromQuery] PaginacionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerPaises(dto));
        }
    }
}
