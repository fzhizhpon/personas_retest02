using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/departamentos")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentosService _service;

        public DepartamentosController(IDepartamentosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerDepartamentos()
        {
            return Ok(await _service.ObtenerDepartamentos());
        }
    }
}
