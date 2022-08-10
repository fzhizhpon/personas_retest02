using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.TelefonoMovil;
using Personas.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/telefonos-moviles")]
    [ApiController]
    public class TelefonosMovilesController : ControllerBase
    {
        protected readonly ITelefonoMovilService _service;

        public TelefonosMovilesController(ITelefonoMovilService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar un teléfono móvil.
        /// </summary>
        /// <param name="dto">Objeto: GuardarTelefonoMovilDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarTelefonoMovil(GuardarTelefonoMovilDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarTelefonoMovil(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una lista de teléfonos moviles dependiendo los parámetros
        /// </summary>
        /// <param name="dto">Objeto: ObtenerTelefonosMovilDto.</param>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTelefonosMovil([FromQuery] ObtenerTelefonosMovilDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerTelefonosMovil(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar un teléfono móvil.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarTelefonoMovilDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarTelefonoMovil(ActualizarTelefonoMovilDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ActualizarTelefonoMovil(dto));
        }

        /// <summary>
        /// Permite eliminar telefono móvil dado el código de del teléfono movil y persona.
        /// </summary>
        /// <param name="dto">Objeto: EliminarTelefonoMovilDto.</param>
        [HttpPut("estado")]
        public async Task<ActionResult<Respuesta>> EliminarTelefonoMovil(EliminarTelefonoMovilDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.EliminarTelefonoMovil(dto));
        }
    }
}
