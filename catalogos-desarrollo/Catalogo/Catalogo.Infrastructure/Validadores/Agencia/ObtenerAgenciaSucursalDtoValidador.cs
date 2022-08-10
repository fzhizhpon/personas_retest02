using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.Agencia
{
    public class ObtenerAgenciaSucursalDtoValidador : AbstractValidator<ObtenerAgenciaSucursalDto>
    {
        public ObtenerAgenciaSucursalDtoValidador()
        {
            RuleFor(x => x.codigoSucursal).NotEmpty();
        }
    }
}
