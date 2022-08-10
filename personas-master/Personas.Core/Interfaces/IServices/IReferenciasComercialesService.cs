using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Core.App;
using Personas.Core.Dtos;
using Personas.Core.Dtos.ReferenciasComerciales;

namespace Personas.Core.Interfaces.IServices
{
    public interface IReferenciasComercialesService
    {

        #region CRUD Referencias Comerciales

        Task<Respuesta> GuardarReferenciaComercial(GuardarReferenciaComercialDto dto);
        Task<Respuesta> ActualizarReferenciaComercial(ActualizarReferenciaComercialDto dto);
        Task<Respuesta> EliminarReferenciaComercial(EliminarReferenciaComercialDto dto);
        Task<Respuesta> ObtenerReferenciaComercial(ObtenerReferenciaComercialDto dto);
        Task<Respuesta> ObtenerReferenciasComerciales(ObtenerReferenciasComercialesDto dto);

        #endregion CRUD Referencias Comerciales
    }
}
