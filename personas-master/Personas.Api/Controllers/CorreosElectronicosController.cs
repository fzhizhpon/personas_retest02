using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.CorreosElectronicos;
using Personas.Core.Interfaces.IServices;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class CorreosElectronicosController : ControllerBase
    {
        private readonly ICorreosElectronicosService _service;

        public CorreosElectronicosController(ICorreosElectronicosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite obtener una colección de correos electrónicos pertenecientes a una persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerCorreos([FromQuery] int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerCorreos(codigoPersona);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite guardar un Correo Electrónico.
        /// </summary>
        /// <param name="dto">Obejto: AgregarCorreoElectronicoDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> AgregarCorreo([FromBody] AgregarCorreoElectronicoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.AgregarCorreo(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar un Correo Electrónico.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarCorreoElectronicoDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarCorreo([FromBody] ActualizarCorreoElectronicoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarCorreo(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar un Correo Electrónico.
        /// </summary>
        /// <param name="dto">Objeto: EliminarCorreoRequest.</param>
        [HttpDelete]
        public async Task<ActionResult<Respuesta>> EliminarCorreo([FromQuery] EliminarCorreoRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarCorreo(dto);

            return Ok(respuesta);
        }
    }
}