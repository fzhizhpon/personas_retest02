using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly IAgenciaService _service;

        public AgenciasController(IAgenciaService service)
        {
            _service = service;
        }

        [HttpGet("/agenciaPorSucursal")]
        public async Task<ActionResult<Respuesta>> ObtenerAgenciasPorSucursal([FromQuery] ObtenerAgenciaSucursalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerAgenciasPorSucursal(dto));
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerAgencias()
        {
            return Ok(await _service.ObtenerAgencias());
        }
    }
}