using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesMuebles;

namespace Personas.Infrastructure.Validadores.BienesMuebles
{
    public class GuardarBienesMueblesDtoValidador : AbstractValidator<GuardarBienesMueblesDto>
    {
        public GuardarBienesMueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.tipoBienMueble)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoReferencia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(20).WithMessage(ValidadorEventos.MAX_TAMANO_VIENTE);
            RuleFor(x => x.descripcion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.avaluoComercial)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
        }
    }
}