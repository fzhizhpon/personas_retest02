using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposSociedadesController : ControllerBase
    {
        private readonly ITipoSociedadService _service;

        public TiposSociedadesController(ITipoSociedadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposSociedades()
        {
            return Ok(await _service.ObtenerTiposSociedades());
        }
    }
}
