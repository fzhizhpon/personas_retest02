using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesIntangibles;

namespace Personas.Infrastructure.Validadores.BienesIntangibles
{
    public class EliminarBienesIntangiblesDtoValidador : AbstractValidator<EliminarBienesIntangiblesDto>
    {
        public EliminarBienesIntangiblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}