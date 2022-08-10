using Catalogo.Core.DTOs.Profesion;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Profesion
{
    public class ActualizarProfesionDtoValidador : AbstractValidator<ActualizarProfesionDto>
    {
        public ActualizarProfesionDtoValidador()
        {
            RuleFor(x => x.codigo).NotEmpty();
            RuleFor(x => x.nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.observacion).MaximumLength(100);
            RuleFor(x => x.estado).InclusiveBetween('0', '1');
            RuleFor(x => x.codigoAlterno).MaximumLength(10);
        }
    }
}
