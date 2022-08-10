using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-etnias")]
    [ApiController]
    public class TiposEtniasController : ControllerBase
    {
        private readonly ITipoEtniaService _service;

        public TiposEtniasController(ITipoEtniaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposEtnias()
        {
            return Ok(await _service.ObtenerTiposEtnias());
        }
    }
}
