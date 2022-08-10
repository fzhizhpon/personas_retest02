using Catalogo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ITipoRepresentanteService
    {
        Task<Respuesta> ObtenerTiposRepresentantes();
    }
}
