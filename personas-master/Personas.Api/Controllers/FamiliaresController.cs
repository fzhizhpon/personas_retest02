using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Familiares;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliaresController : ControllerBase
    {


        protected readonly IFamiliaresService _service;

        public FamiliaresController(IFamiliaresService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar un Familiar.
        /// </summary>
        /// <param name="dto">Objeto: GuardarFamiliarDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarFamiliar(GuardarFamiliarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarFamiliar(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar un Familiar.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarFamiliarDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarFamiliar(ActualizarFamiliarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarFamiliar(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una colección de familiares pertenecientes a una persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerFamiliares(int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerFamiliares(new ObtenerFamiliaresDto()
            {
                codigoPersona = codigoPersona
            });

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener unfamiliar específico pertenecientes a una persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona a la cual se le va a asignar o agregar sus familiares .</param>
        /// <param name="codigoPersonaFamiliar">Código de persona que será asignado como familiar de una persona.</param>
        [HttpGet("{codigoPersona}/{codigoPersonaFamiliar}")]
        public async Task<ActionResult<Respuesta>> ObtenerFamiliar(int codigoPersona, int codigoPersonaFamiliar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerFamiliar(new ObtenerFamiliarDto()
            {
                codigoPersonaFamiliar = codigoPersonaFamiliar,
                codigoPersona = codigoPersona
            });

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar un Familiar.
        /// </summary>
        /// <param name="dto">Objeto: EliminarFamiliarDto.</param>
        [HttpPut("eliminar")]
        public async Task<ActionResult<Respuesta>> EliminarFamiliar(EliminarFamiliarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarFamiliar(dto);

            return Ok(respuesta);
        }

    }
}
