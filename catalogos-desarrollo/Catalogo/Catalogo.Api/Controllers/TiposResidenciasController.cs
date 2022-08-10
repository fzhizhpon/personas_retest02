using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposResidenciasController : ControllerBase
    {
        private readonly ITipoResidenciaService _service;

        public TiposResidenciasController(ITipoResidenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposResidencias()
        {
            return Ok(await _service.ObtenerTiposResidencias());
        }
    }
}
