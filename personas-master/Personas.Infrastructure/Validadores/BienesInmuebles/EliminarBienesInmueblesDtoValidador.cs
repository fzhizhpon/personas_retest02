using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesInmuebles;

namespace Personas.Infrastructure.Validadores.BienesInmuebles
{
    public class EliminarBienesInmueblesDtoValidador: AbstractValidator<EliminarBienesInmueblesDto>
    {
        public EliminarBienesInmueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}