using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Industria
{
    public class ObtenerIndustriaDtoValidador : AbstractValidator<ObtenerIndustriaDto>
    {
        public ObtenerIndustriaDtoValidador()
        {
            RuleFor(x => x.sectorEconomico).NotEmpty();
            RuleFor(x => x.subSectorEconomico).NotEmpty();
        }
    }
}
