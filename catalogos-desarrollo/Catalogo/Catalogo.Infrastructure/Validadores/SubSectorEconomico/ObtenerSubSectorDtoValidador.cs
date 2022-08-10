using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.SubSectorEconomico
{
    public class ObtenerSubSectorDtoValidador : AbstractValidator<ObtenerSubSectorDto>
    {
        public ObtenerSubSectorDtoValidador()
        {
            RuleFor(x => x.sectorEconomico).NotEmpty();
        }
    }
}
