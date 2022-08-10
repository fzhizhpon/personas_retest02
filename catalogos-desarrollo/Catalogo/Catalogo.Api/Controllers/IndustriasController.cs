using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustriasController : ControllerBase
    {
        private readonly IIndustriaService _service;

        public IndustriasController(IIndustriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerIndustriaPorSubSectorEconomico([FromQuery] ObtenerIndustriaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerIndustriaPorSubSectorEconomico(dto));
        }
    }
}