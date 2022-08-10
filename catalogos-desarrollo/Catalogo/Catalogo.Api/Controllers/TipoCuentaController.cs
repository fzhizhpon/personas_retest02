using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/TipoCuenta")]
    [ApiController]
    public class TipoCuentaController : ControllerBase
    {
        private readonly ITipoCuentaService _service;

        public TipoCuentaController(ITipoCuentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTipoCuenta()
        {
            return Ok(await _service.ObtenerTipoCuentas());
        }
    }
}
