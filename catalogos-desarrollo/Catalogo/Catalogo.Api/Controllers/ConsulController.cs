using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
