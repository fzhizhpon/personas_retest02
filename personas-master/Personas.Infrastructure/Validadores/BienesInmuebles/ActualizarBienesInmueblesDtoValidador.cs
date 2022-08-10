using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesInmuebles;

namespace Personas.Infrastructure.Validadores.BienesInmuebles
{
    public class ActualizarBienesInmueblesDtoValidador : AbstractValidator<ActualizarBienesInmueblesDto>
    {
        public ActualizarBienesInmueblesDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.numeroRegistro)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.tipoBienInmueble)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            //RuleFor(x => x.codigoPais).NotEmpty();
            //RuleFor(x => x.codigoProvincia).NotEmpty();
            //RuleFor(x => x.codigoCiudad).NotEmpty();
            //RuleFor(x => x.codigoParroquia).NotEmpty();

            //RuleFor(x => x.sector).NotEmpty().MaximumLength(50);
            RuleFor(x => x.callePrincipal)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.calleSecundaria)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            //RuleFor(x => x.numero).NotEmpty().MaximumLength(10);
            //RuleFor(x => x.codigoPostal).NotEmpty().MaximumLength(10);

            //RuleFor(x => x.tipoSector).NotNull().WithMessage(ValidadorEventos.NO_NULO).NotEmpty().Custom((value, context) =>
            //{
            //    if (value.ToString().Equals('U') || value.ToString().Equals('R'))
            //    {
            //        context.AddFailure("Debe ingresar un código del tipo estado válido.");
            //    }
            //});
            //RuleFor(x => x.esMarginal).InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);

            //RuleFor(x => x.longitud).NotEmpty();
            //RuleFor(x => x.latitud).NotEmpty();
            //RuleFor(x => x.areaTerreno).NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.areaConstruccion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.valorTerrenoMetrosCuadrados)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.fechaConstruccion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);

            RuleFor(x => x.referencia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(200).WithMessage(ValidadorEventos.MAX_TAMANO_DOCIENTOS);
            RuleFor(x => x.descripcion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(100).WithMessage(ValidadorEventos.MAX_TAMANO_CIEN);

            RuleFor(x => x.avaluoComercial)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
            RuleFor(x => x.avaluoCatastral)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
            RuleFor(x => x.valorTerrenoMetrosCuadrados)
                .GreaterThan(0).WithMessage(ValidadorEventos.MAYOR_QUE_CERO);
        }
    }
}