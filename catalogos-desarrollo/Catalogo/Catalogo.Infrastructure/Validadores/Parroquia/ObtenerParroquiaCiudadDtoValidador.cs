using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Parroquia
{
    public class ObtenerParroquiaCiudadDtoValidador : AbstractValidator<ObtenerParroquiaCiudad>
    {
        public ObtenerParroquiaCiudadDtoValidador()
        {
            RuleFor(x => x.codigoPais).NotEmpty();
            RuleFor(x => x.codigoProvincia).NotEmpty();
            RuleFor(x => x.codigoCiudad).NotEmpty();
        }
    }
}
