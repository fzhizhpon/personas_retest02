using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.TablasComunes;

namespace Personas.Infrastructure.Validadores.InformacionAdicional
{
    public class GuardarInformacionAdicionalDtoValidador : AbstractValidator<GuardarInformacionAdicionalDto>
    {
        public GuardarInformacionAdicionalDtoValidador()
        {
            RuleFor(x => x.codigoReferencia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTabla)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.codigoElemento)
                .GreaterThanOrEqualTo(0).WithMessage(ValidadorEventos.MAYOR_O_IGUAL_CERO);
            RuleFor(x => x.observacion)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.codigoModulo)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
        }
    }
}