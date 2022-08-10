using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Provincia
{
    public class ProvinciaValidador : AbstractValidator<Core.Entities.Provincia>
    {
        public ProvinciaValidador()
        {
            RuleFor(x=> x.codigoPais).NotEmpty();
            RuleFor(x=> x.descripcion).NotEmpty().MaximumLength(80);
            RuleFor(x => x.prefijoNumTelefono).MaximumLength(3);
            RuleFor(x => x.estado).InclusiveBetween('0', '1');
            RuleFor(x => x.codigoAlerno).MaximumLength(10);
        }
    }
}
