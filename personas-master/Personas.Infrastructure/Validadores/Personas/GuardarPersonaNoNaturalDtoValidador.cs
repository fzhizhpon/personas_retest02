﻿using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;

namespace Personas.Infrastructure.Validadores.Personas
{
    public class GuardarPersonaNoNaturalDtoValidador : AbstractValidator<GuardarPersonaNoNaturalDto>
    {
        public GuardarPersonaNoNaturalDtoValidador()
        {
            RuleFor(x => x.numeroIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(15).WithMessage(ValidadorEventos.MAX_TAMANO_QUINCE);
            RuleFor(x => x.observaciones)
                .MaximumLength(500).WithMessage(ValidadorEventos.MAX_TAMANO_QUININETOS);
            RuleFor(x => x.codigoTipoIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.razonSocial)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(500).WithMessage(ValidadorEventos.MAX_TAMANO_QUININETOS);
            RuleFor(x => x.fechaConstitucion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.objetoSocial)
                .MaximumLength(150).WithMessage(ValidadorEventos.MAX_TAMANO_CIENTOCINCUENTA);
            RuleFor(x => x.finalidadLucro)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);
            RuleFor(x => x.obligadoLlevarContabilidad)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);
            RuleFor(x => x.agenteRetencion)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);
        }
    }
}