using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.EstadosFinancieros;
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
    public class EstadosFinancierosController : ControllerBase
    {
        protected readonly IEstadosFinancierosService _service;

        public EstadosFinancierosController(IEstadosFinancierosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Permite guardar un Estado financiero.
        /// </summary>
        /// <param name="dto">Objeto: GuardarEstadoFinancieroDto.</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarEstadosFinancieros(GuardarEstadoFinancieroDto dto)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.GuardarEstadosFinancieros(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite actualizar un Estado financiero.
        /// </summary>
        /// <param name="dto">Objeto: ActualizarEstadoFinancieroDto.</param>
        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarEstadosFinancieros(ActualizarEstadoFinancieroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ActualizarEstadosFinancieros(dto);

            return Ok(respuesta);
        }

        /// <summary>
        /// Permite obetner una lista de estados financieros de una persona.
        /// </summary>
        /// <param name="codigoPersona">Código de la persona.</param>
        /// <param name="tipoCuenta">Tipo de cuenta.</param>
        /// <remarks>
        /// El tipo de cuenta puede tomar los valores de:
        /// 'A' => Activos.
        /// 'P' => Pasivos.
        /// 'I' => Ingresos.
        /// 'G' => Gastos.
        /// </remarks>
        [HttpGet("{codigoPersona}/{tipoCuenta}")]
        public async Task<ActionResult<Respuesta>> ObtenerCuentasEstadosFinancieros(int codigoPersona, char tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _service.ObtenerCuentasEstadosFinancieros(new ObtenerCuentasEstadoFinancieroDto()
            {
                codigoPersona = codigoPersona,
                tipoCuenta = tipoCuenta
            });

            return Ok(respuesta);
        }

    }
}
