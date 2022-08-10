using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class NaturalesController : ControllerBase
    {
        private readonly IPersonaNaturalService _service;

        public NaturalesController(IPersonaNaturalService service)
        {
            _service = service;
        }
        /// <summary>
        /// Permite obtener la información detallada de una persona natural.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("informacion/{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerInfoPesona(long codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _service.ObtenerInfoPesona(codigoPersona));
        }

        /// <summary>
        /// Permite obtener la información detallada de una persona natural.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerPersonaNatural(long codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _service.ObtenerPersonaNatural(codigoPersona));
        }

        /// <summary>
        /// Permite guardar una persona natural.
        /// </summary>
        /// <param name="dto">Objeto GuardarPersonaNaturalDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarPersonaNatural(GuardarPersonaNaturalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarPersonaNatural(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar una persona natural.
        /// </summary>
        /// <param name="dto">Objeto ActualizarPersonaNaturalDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarPersonaNatural(ActualizarPersonaNaturalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ActualizarPersonaNatural(dto));
        }
    }
}
