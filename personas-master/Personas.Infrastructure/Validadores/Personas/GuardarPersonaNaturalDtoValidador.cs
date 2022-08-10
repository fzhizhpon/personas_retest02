using FluentValidation;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Interfaces.IRepositories;

namespace Personas.Infrastructure.Validadores.Personas
{
    public class GuardarPersonaNaturalDtoValidador : AbstractValidator<GuardarPersonaNaturalDto>
    {
        protected readonly ConfiguracionApp _config;
        protected readonly IMensajesRespuestaRepository _textoInfoService;

        public GuardarPersonaNaturalDtoValidador(ConfiguracionApp config, IMensajesRespuestaRepository textoInfoService)
        {
            RuleFor(x => x.numeroIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO)
                .MaximumLength(15).WithMessage(ValidadorEventos.MAX_TAMANO_QUINCE).Custom((value, context) =>
                {
                    if (value.ToString().Equals('U') || value.ToString().Equals('R'))
                    {
                        _textoInfoService.ObtenerTextoInfo(
                            config.Idioma,
                            PersonasNaturalesEventos.ACTUALIZAR_CONYUGE_PERSONA_NATURAL_ERROR,
                            config.Modulo);

                        context.AddFailure("Debe ingresar un código del tipo estado válido.");
                    }
                });

            RuleFor(x => x.observaciones)
                .MaximumLength(500).WithMessage(ValidadorEventos.MAX_TAMANO_QUININETOS);
            RuleFor(x => x.codigoTipoIdentificacion)
                .NotEmpty().WithMessage(ValidadorEventos.NO_VACIO);
            RuleFor(x => x.codigoTipoPersona)
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