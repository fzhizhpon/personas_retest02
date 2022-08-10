using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;

namespace Personas.Infrastructure.Validadores.Personas
{
    public class ActualizarPersonaDtoValidador : AbstractValidator<ActualizarPersonaDto>
    {
        public ActualizarPersonaDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(15).WithMessage(ValidadorEventos.MAX_TAMANO_QUINCE);
            RuleFor(x => x.observaciones)
                .MaximumLength(500).WithMessage(ValidadorEventos.MAX_TAMANO_QUININETOS);
            RuleFor(x => x.codigoTipoIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoContribuyente)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}