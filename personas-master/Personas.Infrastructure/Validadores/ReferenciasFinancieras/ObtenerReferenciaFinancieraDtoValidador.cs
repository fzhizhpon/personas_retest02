using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasFinancieras;

namespace Personas.Infrastructure.Validadores
{
    public class ObtenerReferenciaFinancieraDtoValidador : AbstractValidator<ObtenerReferenciaFinancieraDto>
    {
        public ObtenerReferenciaFinancieraDtoValidador()
        {
            RuleFor(x => x.numeroRegistro)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}