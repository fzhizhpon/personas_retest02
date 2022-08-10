using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-autenticacion")]
    [ApiController]
    public class TiposAutenticaiconController : ControllerBase
    {
        private readonly ITipoAutenticacionService _service;

        public TiposAutenticaiconController(ITipoAutenticacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposAutenticacion()
        {
            return Ok(await _service.ObtenerTipoAutenticacion());
        }
    }
}
