using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesEconomicasController : ControllerBase
    {
        private readonly IActividadesEconomicasService _service;

        public ActividadesEconomicasController(IActividadesEconomicasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerPaises([FromQuery] ActividadComercialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ObtenerActividadesEconomicas(dto));
        }

    }
}
