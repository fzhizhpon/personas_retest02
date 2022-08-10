using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Direcciones;

namespace Personas.Infrastructure.Validadores.Direcciones
{
    public class GuardarDireccionDtoValidador : AbstractValidator<GuardarDireccionDto>
    {
        public GuardarDireccionDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPais)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoProvincia)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoCiudad)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoParroquia)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.callePrincipal)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.calleSecundaria)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.referencia)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.sector)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.esMarginal)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO).Custom((value, context) =>
                {
                    if (value != '0' && value != '1')
                    {
                        //context.AddFailure("El campo esMarginal debe tener el valor '1' o '0'.");
                        context.AddFailure(ValidadorEventos.ENTRE_CERO_UNO);
                    }
                });

            RuleFor(x => x.tipoSector)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO).Custom((value, context) =>
                {
                    if (value != 'U' && value != 'R')
                    {
                        //context.AddFailure("El campo tipoSector debe tener el valor 'U' (urbano) o 'R' (rural).");
                        context.AddFailure(ValidadorEventos.CODIGO_U_R);
                    }
                });

            RuleFor(x => x.principal)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO).Custom((value, context) =>
                {
                    if (value != '0' && value != '1')
                    {
                        //context.AddFailure("El campo principal debe tener el valor '1' o '0'.");
                        context.AddFailure(ValidadorEventos.ENTRE_CERO_UNO);
                    }
                });
        }
    }
}