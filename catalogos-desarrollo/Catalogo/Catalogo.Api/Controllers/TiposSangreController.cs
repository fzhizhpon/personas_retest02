using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-sangre")]
    [ApiController]
    public class TiposSangreController : ControllerBase
    {
        private readonly ITipoSangreService _service;

        public TiposSangreController(ITipoSangreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposSangre()
        {
            return Ok(await _service.ObtenerTiposSangre());
        }
    }
}
