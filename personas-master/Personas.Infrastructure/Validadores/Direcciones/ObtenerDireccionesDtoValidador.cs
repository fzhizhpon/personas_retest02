using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Direcciones;

namespace Personas.Infrastructure.Validadores.Direcciones
{
    public class ObtenerDireccionesDtoValidador : AbstractValidator<ObtenerDireccionesDto>
    {
        public ObtenerDireccionesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}