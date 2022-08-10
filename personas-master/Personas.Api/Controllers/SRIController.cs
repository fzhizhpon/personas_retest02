using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personas.Application.Middleware;
using Personas.Core.App;
using Personas.Core.Dtos.Sri;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas.Api.Controllers
{
    [ServiceFilter(typeof(FiltroAuditoria))]
    [Route("api/sri")]
    [ApiController]
    public class SRIController : ControllerBase
    {

        protected readonly ISriService _serviceSRI;

        public SRIController(ISriService serviceSri)
        {
            _serviceSRI = serviceSri;
        }

        [HttpGet("ruc/{numeroRuc}")]
        public async Task<ActionResult<Respuesta>> ObtenerContribuyenteConsolidado(string numeroRuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceSRI.ObtenerContribuyenteConsolidado(new ObtenerContribuyenteConsolidadoDto()
            {
                numeroRuc = numeroRuc
            });
            return Ok(respuesta);
        }

        [HttpGet("placa/{codigoPlaca}")]
        public async Task<ActionResult<Respuesta>> ObtenerInformacionPlaca(string codigoPlaca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Respuesta respuesta = await _serviceSRI.ObtenerInformacionPlaca(new ObtenerInformacionPlacaDto()
            {
                codigoPlaca = codigoPlaca
            });
            return Ok(respuesta);
        }

    }

}
