using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasFinancieras;

namespace Personas.Infrastructure.Validadores.ReferenciasComerciales
{
    public class EliminarReferenciaFinancieraDtoValidador : AbstractValidator<EliminarReferenciaFinancieraDto>
    {
        public EliminarReferenciaFinancieraDtoValidador()
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