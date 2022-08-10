using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Profesion;
using Catalogo.Core.Entities;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionesController : ControllerBase
    {
        private readonly IProfesionService _service;

        public ProfesionesController(IProfesionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> ObtenerProfesiones()
        {
            return Ok(await _service.ObtenerProfesiones());
        }

        [HttpGet("{codigoProfesion}")]
        public async Task<ActionResult<Respuesta>> ObtenerProfesion(int codigoProfesion)
        {
            return Ok(await _service.ObtenerProfesion(codigoProfesion));
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> GuardarProfesion(Profesion profesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.GuardarProfesion(profesion));
        }

        [HttpDelete("{codigoProfesion}")]
        public async Task<ActionResult<Respuesta>> EliminarProfesion(int codigoProfesion)
        {
            return Ok(await _service.EliminarProfesion(codigoProfesion));
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> ActualizarProfesion(ActualizarProfesionDto profesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.ActualizarProfesion(profesion));
        }
    }
}
