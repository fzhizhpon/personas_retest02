using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos;
using Personas.Core.Dtos.ReferenciasPersonales;

namespace Personas.Infrastructure.Validadores.ReferenciasPersonales
{
    public class ObtenerReferenciaPersonalDtoValidador : AbstractValidator<ObtenerReferenciaPersonalDto>
    {
        public ObtenerReferenciaPersonalDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}