using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposContribuyentesController : ControllerBase
    {
        private readonly ITipoContribuyenteService _service;

        public TiposContribuyentesController(ITipoContribuyenteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposContribuyentes()
        {
            return Ok(await _service.ObtenerTiposContribuyentes());
        }
    }
}
