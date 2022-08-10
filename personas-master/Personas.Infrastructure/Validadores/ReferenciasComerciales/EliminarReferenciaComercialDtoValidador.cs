﻿using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasComerciales;

namespace Personas.Infrastructure.Validadores.ReferenciasComerciales
{
    public class EliminarReferenciaComercialDtoValidador : AbstractValidator<EliminarReferenciaComercialDto>
    {
        public EliminarReferenciaComercialDtoValidador()
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