using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.BienesMuebles;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class BienesMueblesController : ControllerBase
    {
        // * propiedad
        private readonly IBienesMueblesService _bienesMueblesService;

        public BienesMueblesController(IBienesMueblesService bienesMueblesService)
        {
            _bienesMueblesService = bienesMueblesService;
        }


        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienesMuebles(long codigoPersona)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesMueblesService.ObtenerBienesMuebles(codigoPersona));
        }

        [HttpGet("{codigoPersona}/{numeroRegistro}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienMueble(long codigoPersona, long numeroRegistro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesMueblesService.ObtenerBienMueble(codigoPersona, numeroRegistro));
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarBienesMuebles(GuardarBienesMueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesMueblesService.GuardarBienesMuebles(dto));
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarBienesMuebles(ActualizarBienesMueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesMueblesService.ActualizarBienesMuebles(dto));
        }

        [HttpPut("estado")]
        public async Task<ActionResult<Respuesta>> EliminarBienesMuebles(EliminarBienesMueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesMueblesService.EliminarBienesMuebles(dto));
        }
    }
}