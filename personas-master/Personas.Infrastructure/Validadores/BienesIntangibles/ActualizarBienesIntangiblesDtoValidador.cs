using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesIntangibles;

namespace Personas.Infrastructure.Validadores.BienesIntangibles
{
    public class ActualizarBienesIntangiblesDtoValidador : AbstractValidator<ActualizarBienesIntangiblesDto>
    {
        public ActualizarBienesIntangiblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.descripcion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.avaluoComercial)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
        }
    }
}