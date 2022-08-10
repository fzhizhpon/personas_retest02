using Catalogo.Core.DTOs.Provincia;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Provincia
{
    public class ObtenerProvinciaPaisDtoValidador : AbstractValidator<ObtenerProvinciaPais>
    {
        public ObtenerProvinciaPaisDtoValidador()
        {
            RuleFor(x => x.codigoPais).NotNull().NotEmpty();
        }
    }
}
