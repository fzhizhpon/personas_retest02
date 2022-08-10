using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.TelefonoMovil;

namespace Personas.Infrastructure.Validadores.TelefonosMoviles
{
    public class EliminarTelefonoMovilDtoValidador : AbstractValidator<EliminarTelefonoMovilDto>
    {
        public EliminarTelefonoMovilDtoValidador()
        {
            RuleFor(x => x.codigoTelefonoMovil)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}