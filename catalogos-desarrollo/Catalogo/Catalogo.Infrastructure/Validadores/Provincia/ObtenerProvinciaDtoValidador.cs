using Catalogo.Core.DTOs.Provincia;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Provincia
{
    public class ObtenerProvinciaDtoValidador : AbstractValidator<ObtenerProvinciaDto>
    {
        public ObtenerProvinciaDtoValidador()
        {
            RuleFor(x => x.codigoPais).NotEmpty();
            RuleFor(x => x.codigoProvincia).NotEmpty();
        }
    }
}
