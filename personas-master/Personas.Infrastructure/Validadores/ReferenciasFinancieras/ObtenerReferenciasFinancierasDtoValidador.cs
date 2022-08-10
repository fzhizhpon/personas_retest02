using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasFinancieras;

namespace Personas.Infrastructure.Validadores
{
    public class ObtenerReferenciasFinancierasDtoValidador : AbstractValidator<ObtenerReferenciasFinancierasDto>
    {
        public ObtenerReferenciasFinancierasDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}