using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Representantes;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentantesController : ControllerBase
    {
        protected readonly IRepresentantesService _service;

        public RepresentantesController(IRepresentantesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar un Representante.
        /// </summary>
        /// <param name="dto">Objeto: GuardarRepresentanteDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarRepresentante(GuardarRepresentanteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarRepresentante(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una colección de representantes pertenecientes a una persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerRepresentantes(int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerRepresentantes(codigoPersona);

            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerRepresentantesFiltros(
            [FromQuery] RepresentanteRequest representanteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerRepresentantesFiltros(representanteRequest);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar un Representante.
        /// </summary>
        /// <param name="dto">Objeto: EliminarRepresentanteDto.</param>
        [HttpPut("eliminar")]
        public async Task<ActionResult<Respuesta>> EliminarRepresentante(EliminarRepresentanteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarRepresentante(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar un Representante.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarRepresentanteDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarRepresentante(ActualizarRepresentanteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarRepresentante(dto);

            return Ok(respuesta);
        }
    }
}