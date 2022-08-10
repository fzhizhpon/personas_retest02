using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/TipoFirmante")]
    [ApiController]
    public class TipoFirmanteController : ControllerBase
    {
        private readonly ITipoFirmanteService _service;

        public TipoFirmanteController(ITipoFirmanteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTipoFirmante()
        {
            return Ok(await _service.ObtenerTipoFirmantes());
        }
    }
}
