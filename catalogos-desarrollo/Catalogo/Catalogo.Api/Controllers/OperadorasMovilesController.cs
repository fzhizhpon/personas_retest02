using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/operadoras-moviles")]
    [ApiController]
    public class OperadorasMovilesController : ControllerBase
    {
        private readonly IOperadoraMovilService _service;

        public OperadorasMovilesController(IOperadoraMovilService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerOperadorasMoviles()
        {
            return Ok(await _service.ObtenerOperadorasMoviles());
        }

        [HttpGet("paises")]
        public async Task<ActionResult<Respuesta>> ObtenerPaisesMarcadoMoviles()
        {
            return Ok(await _service.ObtenerPaisesMarcadoMoviles());
        }
    }
}
