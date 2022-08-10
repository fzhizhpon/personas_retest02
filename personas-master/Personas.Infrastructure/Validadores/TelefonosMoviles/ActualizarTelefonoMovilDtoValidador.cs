using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.TelefonoMovil;

namespace Personas.Infrastructure.Validadores.TelefonosMoviles
{
    public class ActualizarTelefonoMovilDtoValidador : AbstractValidator<ActualizarTelefonoMovilDto>
    {
        public ActualizarTelefonoMovilDtoValidador()
        {
            RuleFor(x => x.codigoTelefonoMovil)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoOperadora)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.observaciones)
                .MaximumLength(250).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOSCINCUENTA);
            RuleFor(x => x.principal)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);
        }
    }
}