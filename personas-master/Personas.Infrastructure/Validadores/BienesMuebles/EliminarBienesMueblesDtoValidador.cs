using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesMuebles;


namespace Personas.Infrastructure.Validadores.BienesMuebles
{
    public class EliminarBienesMueblesDtoValidador : AbstractValidator<EliminarBienesMueblesDto>
    {
        public EliminarBienesMueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}