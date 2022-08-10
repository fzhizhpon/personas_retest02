using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Profesion
{
    public class ProfesionValidador : AbstractValidator<Core.Entities.Profesion>
    {
        public ProfesionValidador()
        {
            RuleFor(x => x.nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.observacion).MaximumLength(100);
            RuleFor(x => x.estado).InclusiveBetween('0', '1');
            RuleFor(x => x.codigoAlterno).MaximumLength(10);
        }
    }
}
