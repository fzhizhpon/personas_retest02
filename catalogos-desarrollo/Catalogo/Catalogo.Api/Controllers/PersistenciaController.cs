using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersistenciaController : ControllerBase
    {

        protected readonly IPersistenciaService _service;

        public PersistenciaController(IPersistenciaService service)
        {
            _service = service;
        }

        [HttpPost("eliminar")]
        public ActionResult<Respuesta> EliminarCatalogos(EliminarPersistenciaDto dto)
        {
            Respuesta respeusta = _service.EliminarCatalogos(dto);
            return respeusta;
        }

    }
}
