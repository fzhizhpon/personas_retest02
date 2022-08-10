using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.ReferenciasFinancieras;
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
    public class ReferenciasFinancierasController : ControllerBase
    {

        protected readonly IReferenciasFinancierasService _service;

        public ReferenciasFinancierasController(IReferenciasFinancierasService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar una referencia financiera.
        /// </summary>
        /// <param name="dto">Objeto: GuardarReferenciaFinancieraDto.</param>        
        [HttpPost("guardar-referencia-financiera")]
        public async Task<ActionResult<Respuesta>> GuardarReferenciaFinanciera(GuardarReferenciaFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarReferenciaFinanciera(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite guardar una referencia financiera.
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciaFinancieraDto.</param>
        [HttpPost("obtener-referencia-financiera")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciaFinanciera(ObtenerReferenciaFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciaFinanciera(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obtener una lista de referencias financieras
        /// </summary>
        /// <param name="dto">Objeto: ObtenerReferenciasFinancierasDto.</param>
        [HttpPost("obtener-referencias-financieras")]
        public async Task<ActionResult<Respuesta>> ObtenerReferenciasFinancieras(ObtenerReferenciasFinancierasDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerReferenciasFinancieras(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite eliminar una referencia financiera.
        /// </summary>
        /// <param name="dto">Objeto: EliminarReferenciaFinancieraDto.</param>        
        [HttpPost("eliminar-referencia-financiera")]
        public async Task<ActionResult<Respuesta>> EliminarReferenciaFinanciera(EliminarReferenciaFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.EliminarReferenciaFinanciera(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar una referencia financiera.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarReferenciaFinancieraDto.</param>        
        [HttpPost("actualizar-referencia-financiera")]
        public async Task<ActionResult<Respuesta>> ActualizarReferenciaFinanciera(ActualizarReferenciaFinancieraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarReferenciaFinanciera(dto);

            return Ok(respuesta);
        }


    }
}
