using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Trabajos;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajosController : ControllerBase
    {

        protected readonly ITrabajosService _service;
        public TrabajosController(ITrabajosService service)
        {
            _service = service;
        }

        [HttpPost("guardar-trabajo")]
        public async Task<ActionResult<Respuesta>> GuardarTrabajo(GuardarTrabajoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarTrabajo(dto);

            return Ok(respuesta);
        }


        [HttpPost("obtener-trabajo")]
        public async Task<ActionResult<Respuesta>> ObtenerTrabajo(ObtenerTrabajoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerTrabajo(dto);

            return Ok(respuesta);
        }

        [HttpPost("obtener-trabajos")]
        public async Task<ActionResult<Respuesta>> ObtenerTrabajos(ObtenerTrabajosDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerTrabajos(dto);

            return Ok(respuesta);
        }

        [HttpPost("eliminar-trabajo")]
        public async Task<ActionResult<Respuesta>> EliminarTrabajo(EliminarTrabajoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarTrabajo(dto);

            return Ok(respuesta);
        }

        [HttpPost("actualizar-trabajo")]
        public async Task<ActionResult<Respuesta>> ActualizarTrabajo(ActualizarTrabajoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarTrabajo(dto);

            return Ok(respuesta);
        }


    }
}
