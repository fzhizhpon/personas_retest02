using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposInstitucionesFinancierasController : ControllerBase
    {
        private readonly ITiposInstitucionesFinancierasService _service;

        public TiposInstitucionesFinancierasController(ITiposInstitucionesFinancierasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposInstitucionesFinancieras()
        {
            return Ok(await _service.ObtenerTiposInstitucionesFinancieras());
        }
    }
}