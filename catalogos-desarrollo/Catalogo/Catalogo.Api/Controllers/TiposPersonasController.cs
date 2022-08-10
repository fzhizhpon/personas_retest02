using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-personas")]
    [ApiController]
    public class TiposPersonasController : ControllerBase
    {
        private readonly ITipoPersonaService _service;

        public TiposPersonasController(ITipoPersonaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposPersonas()
        {
            return Ok(await _service.ObtenerTiposPersona());
        }
    }
}
