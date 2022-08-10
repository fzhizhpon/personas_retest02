using Catalogo.Core.DTOs.Producto;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.InstitucionesFinancieras
{
    public class ObtenerProductoActividadFinancieraDtoValidador : AbstractValidator<ObtenerProductoActividadFinancieraDto>
    {
        public ObtenerProductoActividadFinancieraDtoValidador()
        {
            RuleFor(x => x.codigoMoneda).NotEmpty();
            RuleFor(x => x.codigoActividadFinanciera).NotEmpty();
        }
    }
}