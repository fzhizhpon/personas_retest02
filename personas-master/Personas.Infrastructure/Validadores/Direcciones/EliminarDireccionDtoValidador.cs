using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Direcciones;

namespace Personas.Infrastructure.Validadores.Direcciones
{
    public class EliminarDireccionDtoValidador : AbstractValidator<EliminarDireccionDto>
    {
        public EliminarDireccionDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}