using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesInmuebles;

namespace Personas.Infrastructure.Validadores.BienesInmuebles
{
    public class GuardarBienesInmueblesDtoValidador : AbstractValidator<GuardarBienesInmueblesDto>
    {
        public GuardarBienesInmueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.tipoBienInmueble)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoPais)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoProvincia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoCiudad)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoParroquia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.sector)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO).MaximumLength(50);
            RuleFor(x => x.callePrincipal)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.calleSecundaria)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.numero)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(10).WithMessage(ValidadorEventos.MAX_DIEZ);
            RuleFor(x => x.codigoPostal)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(10).WithMessage(ValidadorEventos.MAX_DIEZ);

            RuleFor(x => x.tipoSector)
                .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO).Custom((value, context) =>
                {
                    if (value.ToString().Equals('U') || value.ToString().Equals('R'))
                    {
                        context.AddFailure(ValidadorEventos.CODIGO_U_R);
                    }
                });

            When(x => x.tipoSector == 'R', () =>
            {
                RuleFor(x => x.comunidad)
                    .NotNull().WithMessage(ValidadorEventos.NO_NULO)
                    .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
                RuleFor(x => x.comunidad)
                    .MaximumLength(100).WithMessage(ValidadorEventos.MAX_TAMANO_CIEN);
            });

            RuleFor(x => x.esMarginal)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);

            RuleFor(x => x.longitud)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.latitud)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.avaluoComercial)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
            RuleFor(x => x.avaluoCatastral)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
            RuleFor(x => x.valorTerrenoMetrosCuadrados)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
            RuleFor(x => x.areaTerreno)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.areaConstruccion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.fechaConstruccion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.referencia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.descripcion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(100).WithMessage(ValidadorEventos.MAX_TAMANO_CIEN);

            /*
NotNull: Se requiere un valor (negativos, positivos o 0)
GreaterThanOrEqualTo: Valor minimo incluyendo el indicado. Ejemplo: GreaterThanOrEqualTo(0) => acepta el 0 y numeros mayores
GreaterThan: Valor minimo, excluye el indicado. Ejemplo: GreaterThan(0) => acepta numeros mayores a 0             
             */
        }
    }
}