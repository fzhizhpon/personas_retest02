using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Personas.Application.Middleware;
using Personas.Core.Interfaces.IServices;
using Personas.Core.App;
using Personas.Core.Dtos.RelacionInstitucion;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionInstitucionController : ControllerBase
    {
        private readonly IRelacionInstitucionService _service;

        public RelacionInstitucionController(IRelacionInstitucionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar una una relacion del usuario con la institucion.
        /// </summary>
        /// <param name="dto">Objeto: PersonaRelacionInstitucion.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Respuesta respuesta = await _service.GuardarPersonaRelacionInstitucion(dto);

            return Ok(respuesta);
        }
       

        /// <summary>
        /// Permite actualizar el estado de una relacion del usuario con la institucion (elimnar o agregar).
        /// </summary>
        /// <param name="dto">Objeto: PersonaRelacionInstitucion.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarPersonaRelacionInstitucion(dto);

            return Ok(respuesta);
        }


        /// <summary>
        /// Permite obtener una lista de relacion del usuario con la institucion dependiendo los parámetros opcionales.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerDireccionesDto.</param>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerRelacionInstitucion([FromQuery] RelacionInstitucion dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerRelacionInstitucion(dto);

            return Ok(respuesta);
        }
    }
}