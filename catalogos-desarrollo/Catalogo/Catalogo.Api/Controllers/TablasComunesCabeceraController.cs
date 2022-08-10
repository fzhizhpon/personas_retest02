using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablasComunesCabeceraController : ControllerBase
    {
        private readonly ITablasComunesCabeceraService _service;

        public TablasComunesCabeceraController(ITablasComunesCabeceraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTablasComunesCabeceras([FromQuery] ObtenerTablasComunesCabeceraDto dto)
        {
            return Ok(await _service.ObtenerTablasComunesCabeceras(dto));
        }
    }
}