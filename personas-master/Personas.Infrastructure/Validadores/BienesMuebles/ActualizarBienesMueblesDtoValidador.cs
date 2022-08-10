﻿using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesMuebles;

namespace Personas.Infrastructure.Validadores.BienesMuebles
{
    public class ActualizarBienesMueblesDtoValidador : AbstractValidator<ActualizarBienesMueblesDto>
    {
        public ActualizarBienesMueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.tipoBienMueble)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoReferencia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(20);
            RuleFor(x => x.descripcion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.avaluoComercial)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
        }
    }
}