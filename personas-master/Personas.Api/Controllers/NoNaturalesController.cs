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
    public class NoNaturalesController : ControllerBase
    {
        private readonly IPersonaNoNaturalService _service;

        public NoNaturalesController(IPersonaNoNaturalService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite obtener la información detallada de una persona no natural.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerPersonaNoNatural(long codigoPersona)
        {
            return Ok(await _service.ObtenerPersonaNoNatural(codigoPersona));
        }

        /// <summary>
        /// Permite guardar una persona no natural.
        /// </summary>
        /// <param name="dto">Objeto: GuardarPersonaNoNaturalDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarPersonaNoNatural(GuardarPersonaNoNaturalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarPersonaNoNatural(dto);
            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar una persona no natural..
        /// </summary>
        /// <param name="dto">Objeto: ActualizarPersonaNoNaturalDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarPersonaNoNatural(ActualizarPersonaNoNaturalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _service.ActualizarPersonaNoNatural(dto));
        }
    }
}
