using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/monedas")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly IMonedaService _service;

        public MonedasController(IMonedaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerMonedas()
        {
            return Ok(await _service.ObtenerMonedas());
        }
    }
}
