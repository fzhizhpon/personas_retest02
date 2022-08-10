using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Ciudad
{
    public class ObtenerCiudadProvinciaDtoValidador : AbstractValidator<ObtenerCiudadProvincia>
    {
        public ObtenerCiudadProvinciaDtoValidador()
        {
            RuleFor(x => x.codigoPais).NotEmpty();
            RuleFor(x => x.codigoProvincia).NotEmpty();
        }
    }
}
