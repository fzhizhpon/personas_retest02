using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Core.Dtos;
using Personas.Core.Dtos.ReferenciasComerciales;
using Personas.Core.Entities;
using Personas.Core.Entities.ReferenciasComerciales;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface IReferenciasComercialesRepository
    {
        Task<int> GuardarReferenciaComercial(GuardarReferenciaComercialDto dto);
        Task<int> ActualizarReferenciaComercial(ActualizarReferenciaComercialDto dto);
        Task<int> EliminarReferenciaComercial(EliminarReferenciaComercialDto dto);
        Task<ReferenciaComercial> ObtenerReferenciaComercial(ObtenerReferenciaComercialDto dto);
        Task<IList<ReferenciaComercial.ReferenciaComercialMinimo>> ObtenerReferenciasComerciales(ObtenerReferenciasComercialesDto dto);
        Task<int> ObtenerCodigoReferenciaComercial(int codigoPersona);

    }
}
