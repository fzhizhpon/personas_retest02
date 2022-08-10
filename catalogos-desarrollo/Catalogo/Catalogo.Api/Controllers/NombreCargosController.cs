using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/nombre-cargos")]
    [ApiController]
    public class NombreCargosController : ControllerBase
    {
        private readonly INombreCargosService _service;

        public NombreCargosController(INombreCargosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerNombreCargos()
        {
            return Ok(await _service.ObtenerNombreCargos());
        }
    }
}
