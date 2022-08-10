using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablasComunesDetallesController : ControllerBase
    {
        private readonly ITablasComunesDetallesService _service;

        public TablasComunesDetallesController(ITablasComunesDetallesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTablasComunesDetalles(
            [FromQuery] ObtenerTablasComunesDetallesDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerTablasComunesDetalles(dto));
        }
    }
}