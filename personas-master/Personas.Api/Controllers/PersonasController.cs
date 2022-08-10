using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _service;

        public PersonasController(IPersonaService service)
        {
            _service = service;
        }
        /// <summary>
        /// Permite obtener una lista de personas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerPersonas([FromQuery] PersonaRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _service.ObtenerPersonas(dto));
        }

        /// <summary>
        /// Permite obtener una persona, en este caso tenemos diferentes parametros.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerPersona(int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _service.ObtenerPersona(codigoPersona));
        }

        /// <summary>
        /// Permite obtener un objeto persona, en este caso la hemos filtrado por el código de la persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("min/{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerPersonaJoinMinimo(int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerPersonaJoinMinimo(new UltActPersonaRequest
            {
                codigoPersona = codigoPersona
            }));
        }

        /// <summary>
        /// Permite actualizar una persona.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarPersona(ActualizarPersonaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ActualizarPersona(dto));
        }
    }
}
