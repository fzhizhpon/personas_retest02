using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : ControllerBase
    {
        private readonly ICiudadService _service;

        public CiudadesController(ICiudadService service)
        {
            _service = service;
        }

        [HttpGet("provincia")]
        public async Task<ActionResult<Respuesta>> ObtenerCiudadesPorProvincia([FromQuery] ObtenerCiudadProvincia dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerCiudadesPorProvincia(dto));
        }
    }
}
