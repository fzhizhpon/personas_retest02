using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.TelefonoMovil;

namespace Personas.Infrastructure.Validadores.TelefonosMoviles
{
    public class ObtenerTelefonosMovilDtoValidador : AbstractValidator<ObtenerTelefonosMovilDto>
    {
        public ObtenerTelefonosMovilDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}