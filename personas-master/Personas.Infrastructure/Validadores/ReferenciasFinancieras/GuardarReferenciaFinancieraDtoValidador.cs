using FluentValidation;
using Personas.Core.Dtos.ReferenciasComerciales;
using Personas.Core.Dtos.ReferenciasFinancieras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Application.CodigosEventos;

namespace Personas.Infrastructure.Validadores.ReferenciasComerciales
{
    public class GuardarReferenciaFinancieraDtoValidador : AbstractValidator<GuardarReferenciaFinancieraDto>
    {
        public GuardarReferenciaFinancieraDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoCuentaFinanciera)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoInstitucionFinanciera)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.fechaCuenta)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroCuenta)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.cifras)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.saldo)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.saldoObligacion)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
            RuleFor(x => x.obligacionMensual)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO);
        }
    }
}