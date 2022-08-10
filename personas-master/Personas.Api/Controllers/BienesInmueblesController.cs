using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.BienesInmuebles;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class BienesInmueblesController : ControllerBase
    {
        // * propiedad
        private readonly IBienesInmueblesService _bienesInmueblesService;

        public BienesInmueblesController(IBienesInmueblesService bienesInmueblesService)
        {
            _bienesInmueblesService = bienesInmueblesService;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerBienesInmuebles([FromQuery] ObtenerBienesInmueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.ObtenerBienesInmuebles(dto));
        }

        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienesInmueblesSinJoin(long codigoPersona)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.ObtenerBienesInmueblesSinJoin(codigoPersona));
        }


        [HttpGet("{codigoPersona}/{numeroRegistro}")]
        public async Task<ActionResult<Respuesta>> ObtenerBienInmueble(long codigoPersona, long numeroRegistro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.ObtenerBienInmueble(codigoPersona, numeroRegistro));
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarBienesImuebles(GuardarBienesInmueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.GuardarBienesInmuebles(dto));
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarBienesImuebles(ActualizarBienesInmueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.ActualizarBienesInmuebles(dto));
        }

        [HttpPut("estado")]
        public async Task<ActionResult<Respuesta>> EliminarBienesImuebles(EliminarBienesInmueblesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _bienesInmueblesService.EliminarBienesInmuebles(dto));
        }
    }
}