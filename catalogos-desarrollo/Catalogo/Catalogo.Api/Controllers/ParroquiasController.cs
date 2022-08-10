using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParroquiasController : ControllerBase
    {
        private readonly IParroquiaService _service;

        public ParroquiasController(IParroquiaService service)
        {
            _service = service;
        }

        [HttpGet("ciudad")]
        public async Task<ActionResult<Respuesta>> ObtenerParroquiasPorCiudad([FromQuery] ObtenerParroquiaCiudad dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerParroquiasPorCiudad(dto));
        }
    }
}
