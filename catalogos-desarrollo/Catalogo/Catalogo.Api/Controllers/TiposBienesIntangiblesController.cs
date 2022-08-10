using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposBienesIntangiblesController : ControllerBase
    {
        private readonly ITiposBienesIntangiblesService _service;

        public TiposBienesIntangiblesController(ITiposBienesIntangiblesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposBienesIntangibles()
        {
            return Ok(await _service.ObtenerTiposBienesIntangibles());
        }
    }
}