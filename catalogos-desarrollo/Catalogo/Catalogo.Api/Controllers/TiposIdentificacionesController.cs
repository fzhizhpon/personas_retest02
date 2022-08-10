using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-identificaciones")]
    [ApiController]
    public class TiposIdentificacionesController : ControllerBase
    {
        private readonly ITipoIdentificacionService _service;

        public TiposIdentificacionesController(ITipoIdentificacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposIdentificaciones()
        {
            return Ok(await _service.ObtenerTiposIdentificaciones());
        }
    }
}
