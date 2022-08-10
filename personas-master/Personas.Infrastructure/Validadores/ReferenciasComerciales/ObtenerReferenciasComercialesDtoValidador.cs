using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.ReferenciasComerciales;

namespace Personas.Infrastructure.Validadores
{
    public class ObtenerReferenciasComercialesDtoValidador : AbstractValidator<ObtenerReferenciasComercialesDto>
    {
        public ObtenerReferenciasComercialesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.paginacion.indiceInicial)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.paginacion.numeroRegistros)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}