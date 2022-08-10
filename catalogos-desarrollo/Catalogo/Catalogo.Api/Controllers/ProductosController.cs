using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Producto;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _service;

        public ProductosController(IProductosService service)
        {
            _service = service;
        }

        [HttpGet("actividadFinanciera")]
        public async Task<ActionResult<Respuesta>> ObtenerProductosActividadFinanciera(
            [FromQuery] ObtenerProductoActividadFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerProductosActividadFinanciera(dto));
        }
        
        [HttpGet("codigoGrupo")]
        public async Task<ActionResult<Respuesta>> ObtenerProductosCodigoGrupo(
            [FromQuery] ObtenerProductoCodigoGrupoDto dto)
        {
            return Ok(await _service.ObtenerProductosCodigoGrupo(dto));
        }
    }
}