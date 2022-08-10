using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-tiempo")]
    [ApiController]
    public class TiposTiempoController : ControllerBase
    {
        private readonly ITipoTiempoService _service;

        public TiposTiempoController(ITipoTiempoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposTiempo()
        {
            return Ok(await _service.ObtenerTiposTiempo());
        }
    }
}
