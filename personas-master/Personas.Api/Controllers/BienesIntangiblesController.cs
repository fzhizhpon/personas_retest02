using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.BienesIntangibles;
using Personas.Core.Interfaces.IServices;


namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class BienesIntangiblesController : ControllerBase
    {
        // * propiedad
        private readonly IBienesIntangiblesService _bienesIntangiblesService;

        public BienesIntangiblesController(IBienesIntangiblesService bienesIntangiblesService)
        {
            _bienesIntangiblesService = bienesIntangiblesService;
        }

        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienesIntangibles(long codigoPersona)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesIntangiblesService.ObtenerBienesIntangibles(codigoPersona));
        }

        [HttpGet("{codigoPersona}/{numeroRegistro}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienIntangible(long codigoPersona, long numeroRegistro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesIntangiblesService.ObtenerBienIntangible(codigoPersona, numeroRegistro));
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarBienesIntangibles(GuardarBienesIntangiblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesIntangiblesService.GuardarBienesIntangibles(dto));
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarBienesIntangibles(ActualizarBienesIntangiblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesIntangiblesService.ActualizarBienesIntangibles(dto));
        }

        [HttpPut("estado")]
        public async Task<ActionResult<Respuesta>> EliminarBienesIntangibles(EliminarBienesIntangiblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesIntangiblesService.EliminarBienesIntangibles(dto));
        }
    }
}