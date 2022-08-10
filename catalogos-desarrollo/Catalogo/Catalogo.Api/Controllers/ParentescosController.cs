using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentescosController : ControllerBase
    {
        private readonly IParentescoService _service;

        public ParentescosController(IParentescoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerParentescos()
        {
            return Ok(await _service.ObtenerParentescos());
        }
    }
}
