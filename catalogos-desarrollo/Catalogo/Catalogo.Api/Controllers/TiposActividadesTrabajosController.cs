using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-actividades-trabajos")]
    [ApiController]
    public class TiposActividadesTrabajosController : ControllerBase
    {
        private readonly ITipoActividadTrabajoService _service;

        public TiposActividadesTrabajosController(ITipoActividadTrabajoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposActividadesTrabajos()
        {
            return Ok(await _service.ObtenerTiposActividadesTrabajos());
        }
    }
}
