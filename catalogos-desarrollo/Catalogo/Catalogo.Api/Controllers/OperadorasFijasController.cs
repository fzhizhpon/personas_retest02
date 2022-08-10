using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/operadoras-fijas")]
    [ApiController]
    public class OperadorasFijasController : ControllerBase
    {
        private readonly IOperadoraFijaService _service;

        public OperadorasFijasController(IOperadoraFijaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerOperadorasFijas()
        {
            return Ok(await _service.ObtenerOperadorasFijas());
        }
    }
}
