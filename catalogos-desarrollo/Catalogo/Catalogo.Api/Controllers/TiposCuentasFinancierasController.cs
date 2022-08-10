using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/tipos-cuentas-financieras")]
    [ApiController]
    public class TiposCuentasFinancierasController : ControllerBase
    {
        private readonly ITipoCuentaFinancieraService _service;

        public TiposCuentasFinancierasController(ITipoCuentaFinancieraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerTiposCuentasFinancieras()
        {
            return Ok(await _service.ObtenerTiposCuentasFinancieras());
        }
    }
}
