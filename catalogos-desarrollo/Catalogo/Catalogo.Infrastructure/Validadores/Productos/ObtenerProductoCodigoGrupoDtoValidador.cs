using Catalogo.Core.DTOs.Producto;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.InstitucionesFinancieras
{
    public class ObtenerProductoCodigoGrupoDtoValidador : AbstractValidator<ObtenerProductoCodigoGrupoDto>
    {
        public ObtenerProductoCodigoGrupoDtoValidador()
        {
            RuleFor(x => x.codigoMoneda).NotEmpty();
            RuleFor(x => x.codigoGrupo).NotEmpty();
        }
    }
}