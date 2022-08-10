using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personas.Application.Middleware;
using Personas.Core.Dtos;
using Personas.Core.Interfaces.IServices;
using Personas.Core.Dtos.ReferenciasComerciales;
using Personas.Core.App;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciasComercialesController : ControllerBase
    {

        protected readonly IReferenciasComercialesService _service;

        public ReferenciasComercialesController(IReferenciasComercialesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar una referencia comercial.
        /// </summary>
        /// <param name="dto">Objeto: GuardarReferenciaComercialDto.</param>
        [HttpPost("guardar-referencia-comercial")]
        public async Task<ActionResult<Respuesta>> GuardarReferenciaComercial(GuardarReferenciaComercialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarReferenciaComercial(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una lista de referencias comerciales.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciasComercialesDto.</param>
        [HttpPost("obtener-referencias-comerciales")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciasComerciales(ObtenerReferenciasComercialesDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciasComerciales(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una referencia comercial.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciaComercialDto.</param>
        [HttpPost("obtener-referencia-comercial")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciaComercial(ObtenerReferenciaComercialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciaComercial(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar una referencia comercial.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarReferenciaComercialDto.</param>
        [HttpPost("actualizar-referencia-comercial")]
        public async Task<ActionResult<Respuesta>> ActualizarReferenciaComercial(ActualizarReferenciaComercialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarReferenciaComercial(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar una referencia comercial.
        /// </summary>
        /// <param name="dto">Objeto: EliminarReferenciaComercialDto.</param>
        [HttpPost("eliminar-referencia-comercial")]
        public async Task<ActionResult<Respuesta>> EliminarReferenciaComercial(EliminarReferenciaComercialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarReferenciaComercial(dto);

            return Ok(respuesta);
        }

    }
}
