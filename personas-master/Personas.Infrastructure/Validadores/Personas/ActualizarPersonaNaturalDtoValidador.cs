using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;

namespace Personas.Infrastructure.Validadores.Personas
{
    public class ActualizarPersonaNaturalDtoValidador : AbstractValidator<ActualizarPersonaNaturalDto>
    {
        public ActualizarPersonaNaturalDtoValidador()
        {
            RuleFor(x => x.codigoPersona)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.nombres)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(80).WithMessage(ValidadorEventos.MAX_TAMANO_OCHENTA);
            RuleFor(x => x.apellidoPaterno)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(40).WithMessage(ValidadorEventos.MAX_TAMANO_CUARENTA);
            RuleFor(x => x.apellidoMaterno)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(40).WithMessage(ValidadorEventos.MAX_TAMANO_CUARENTA);
            RuleFor(x => x.fechaNacimiento)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.tieneDiscapacidad)
                .InclusiveBetween('0', '1').WithMessage(ValidadorEventos.ENTRE_CERO_UNO);
            RuleFor(x => x.codigoPaisNacimiento)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoProvinciaNacimiento)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoCiudadNacimiento)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoParroquiaNacimiento)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoSangre)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoEstadoCivil)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoGenero)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoProfesion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoEtnia)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
        }
    }
}