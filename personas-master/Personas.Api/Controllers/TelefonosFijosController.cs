using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.TelefonosFijos;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/telefonos-fijos")]
    [ApiController]
    public class TelefonosFijosController : ControllerBase
    {

        protected readonly ITelefonosFijosService _serviceTelFijos;

        public TelefonosFijosController(ITelefonosFijosService serviceTelFijos)
        {
            _serviceTelFijos = serviceTelFijos;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarTelefonoFijo(GuardarTelefonoFijoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceTelFijos.GuardarTelefonoFijo(dto);
            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarTelefonoFijo(ActualizarTelefonoFijoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceTelFijos.ActualizarTelefonoFijo(dto);
            return Ok(respuesta);
        }

        [HttpDelete]
        public async Task<ActionResult<Respuesta>> EliminarTelefonoFijo(EliminarTelefonoFijoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceTelFijos.EliminarTelefonoFijo(dto);
            return Ok(respuesta);
        }

        [HttpGet("{codigoPersona}")]
        public async Task<ActionResult<Respuesta>> ObtenerTelefonosFijos(int codigoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceTelFijos.ObtenerTelefonosFijos(new ObtenerTelefonosFijosDto()
            {
                codigoPersona = codigoPersona
            });
            return Ok(respuesta);
        }

        [HttpGet("{codigoPersona}/{numeroRegistro}")]
        public async Task<ActionResult<Respuesta>> ObtenerTelefonoFijo(int codigoPersona, int numeroRegistro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceTelFijos.ObtenerTelefonoFijo(new ObtenerTelefonoFijoDto()
            {
                codigoPersona = codigoPersona,
                numeroRegistro = numeroRegistro
            });
            return Ok(respuesta);
        }

    }

}
