using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposBienesInmueblesController : ControllerBase
    {
        private readonly ITiposBienesInmueblesService _service;

        public TiposBienesInmueblesController(ITiposBienesInmueblesService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposBienesInmuebles()
        {
            return Ok(await _service.ObtenerTiposBienesInmuebles());
        }
    }
}