using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposBienesMueblesController : ControllerBase
    {
        private readonly ITiposBienesMueblesService _service;

        public TiposBienesMueblesController(ITiposBienesMueblesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposBienesMuebles()
        {
            return Ok(await _service.ObtenerTiposBienesMuebles());
        }
    }
}