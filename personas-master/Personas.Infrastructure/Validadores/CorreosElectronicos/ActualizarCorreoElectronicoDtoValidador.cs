using FluentValidation;
using Personas.Core.Dtos.CorreosElectronicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Application.CodigosEventos;

namespace Personas.Infrastructure.Validadores.CorreosElectronicos
{
    public class ActualizarCorreoElectronicoDtoValidador : AbstractValidator<ActualizarCorreoElectronicoDto>
    {

        public ActualizarCorreoElectronicoDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(s => s.codigoCorreoElectronico)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(s => s.correoElectronico)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage(ValidadorEventos.FORMATO_CORREO);
            RuleFor(x => x.esPrincipal)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }

}
