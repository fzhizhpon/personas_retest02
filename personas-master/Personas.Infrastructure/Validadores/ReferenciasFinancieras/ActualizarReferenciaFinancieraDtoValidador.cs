using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasFinancieras;

namespace Personas.Infrastructure.Validadores.ReferenciasComerciales
{
    public class ActualizarReferenciaFinancieraDtoValidador : AbstractValidator<ActualizarReferenciaFinancieraDto>
    {
        public ActualizarReferenciaFinancieraDtoValidador()
        {
            RuleFor(x => x.numeroRegistro)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.saldo)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.cifras)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.saldoObligacion)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.obligacionMensual)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
        }
    }
}