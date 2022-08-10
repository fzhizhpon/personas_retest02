using Catalogo.Core.DTOs;
using FluentValidation;

namespace Catalogo.Infrastructure.Validadores.InstitucionesFinancieras
{
    public class ObtenerInstitucionFinancieraDtoValidador : AbstractValidator<ObtenerInstitucionFinancieraDto>
    {
        public ObtenerInstitucionFinancieraDtoValidador()
        {
            // RuleFor(x => x.codigoTipoFinanciera).NotEmpty();
        }
    }
}