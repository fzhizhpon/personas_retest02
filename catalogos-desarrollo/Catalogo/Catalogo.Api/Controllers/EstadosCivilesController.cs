using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/estados-civiles")]
    [ApiController]
    public class EstadosCivilesController : ControllerBase
    {
        private readonly IEstadoCivilService _service;

        public EstadosCivilesController(IEstadoCivilService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerEstadosCiviles()
        {
            return Ok(await _service.ObtenerEstadosCiviles());
        }
    }
}