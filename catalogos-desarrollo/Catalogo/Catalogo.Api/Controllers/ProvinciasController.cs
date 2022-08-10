using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Provincia;
using Catalogo.Core.Entities;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly IProvinciaService _service;

        public ProvinciasController(IProvinciaService service)
        {
            _service = service;
        }

        [HttpGet("pais")]
        public async Task<ActionResult<Respuesta>> ObtenerProvinciasPorPais([FromQuery] ObtenerProvinciaPais dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerProvinciasPorPais(dto));
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerProvincia([FromQuery] ObtenerProvinciaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerProvincia(dto));
        }
    }
}
