using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionesFinancierasController : ControllerBase
    {
        private readonly IInstitucionesFinancierasService _service;

        public InstitucionesFinancierasController(IInstitucionesFinancierasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerInstitucionesFinancieras(
            [FromQuery] ObtenerInstitucionFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerInstitucionesFinancieras(dto));
        }
        
        [HttpGet("full")]
        public async Task<ActionResult<Respuesta>> ObtenerInstitucionesFinancierasFull()
        {
            return Ok(await _service.ObtenerInsitucionesFinancierasFull());
        }
    }
}