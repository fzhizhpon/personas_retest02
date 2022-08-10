using FluentValidation;
using Personas.Core.Dtos.EstadosFinancieros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Application.CodigosEventos;

namespace Personas.Infrastructure.Validadores.EstadosFinancieros
{
    class GuardarEstadoFinancieroDtoValidador : AbstractValidator<GuardarEstadoFinancieroDto>
    {
        public GuardarEstadoFinancieroDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.cuentaContable)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.valor)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
        }
    }
}