using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/niveles-instruccion")]
    [ApiController]
    public class NivelesInstruccionController : ControllerBase
    {
        private readonly INivelInstruccionService _service;

        public NivelesInstruccionController(INivelInstruccionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerNivelesInstruccion()
        {
            return Ok(await _service.ObtenerNivelesInstruccion());
        }
    }
}
