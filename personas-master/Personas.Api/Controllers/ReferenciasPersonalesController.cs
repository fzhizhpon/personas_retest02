using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.ReferenciasPersonales;
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
    public class ReferenciasPersonalesController : ControllerBase
    {

        protected readonly IReferenciasPersonalesService _service;

        public ReferenciasPersonalesController(IReferenciasPersonalesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar una referencia personal.
        /// </summary>
        /// <param name="dto">Objeto: GuardarReferenciaPersonalDto.</param>        
        [HttpPost("guardar-referencia-personal")]
        public async Task<ActionResult<Respuesta>> GuardarReferenciaPersonal(GuardarReferenciaPersonalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarReferenciaPersonal(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una referencia personal.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciaPersonalDto.</param>
        [HttpPost("obtener-referencia-personal")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciaPersonal(ObtenerReferenciaPersonalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciaPersonal(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite guardar una lista de referencias personales.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciasPersonales.</param>
        [HttpPost("obtener-referencias-personales")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciasPersonales(ObtenerReferenciasPersonalesDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciasPersonales(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar una referencia personal.
        /// </summary>
        /// <param name="dto">Objeto: EliminarReferenciaPersonalDto.</param>        
        [HttpPost("eliminar-referencia-personal")]
        public async Task<ActionResult<Respuesta>> EliminarReferenciaPersonal(EliminarReferenciaPersonalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarReferenciaPersonal(dto);

            return Ok(respuesta);
        }

        //[HttpPost("actualizar-referencia-personal")]
        //public async Task<ActionResult<Respuesta>> ActualizarReferenciaPersonal(ActualizarReferenciaPersonalDto dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Respuesta respuesta = await _service.ActualizarReferenciaPersonal(dto);

        //    return Ok(respuesta);
        //}

    }
}
