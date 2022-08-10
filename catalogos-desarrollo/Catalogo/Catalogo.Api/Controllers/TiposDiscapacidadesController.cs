using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-discapacidades")]
    [ApiController]
    public class TiposDiscapacidadesController : ControllerBase
    {
        private readonly ITipoDiscapacidadService _service;

        public TiposDiscapacidadesController(ITipoDiscapacidadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposDiscapacidades()
        {
            return Ok(await _service.ObtenerTiposDiscapacidades());
        }
    }
}
