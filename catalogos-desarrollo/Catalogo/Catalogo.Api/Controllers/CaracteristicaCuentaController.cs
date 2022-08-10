using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/CaracteristicaCuenta")]
    [ApiController]
    public class CaracteristicaCuentaController : ControllerBase
    {
        private readonly ICaracteristicaCuentaService _service;

        public CaracteristicaCuentaController(ICaracteristicaCuentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerCaracteristicaCuenta()
        {
            return Ok(await _service.ObtenerCaracteristicaCuentas());
        }
    }
}
