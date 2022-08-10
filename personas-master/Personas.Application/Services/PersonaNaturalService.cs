using System;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;
using VimaCoop.Validadores;

namespace Personas.Application.Services
{
    public class PersonaNaturalService : IPersonaNaturalService
    {
        protected readonly IPersonaRepository _personaRepository;
        protected readonly IPersonaNaturalRepository _personaNaturalRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<PersonaNaturalService> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public PersonaNaturalService(IPersonaNaturalRepository repository,
            IPersonaRepository personaRepository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<PersonaNaturalService> logger,
            ConfiguracionApp config,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _personaNaturalRepository = repository;
            _personaRepository = personaRepository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarPersonaNatural(GuardarPersonaNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = PersonasNaturalesEventos.GUARDAR_PERSONA_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
                long codPersona = 0;

                try
                {
                    dto.fechaUsuarioRegistra = DateTime.Now;
                    dto.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;

                    int nroPersonas =
                        await _personaRepository.ObtenerPersonasPorIdentificacion(dto.numeroIdentificacion);


                    if (nroPersonas != 0)
                    {
                        throw new ExcepcionOperativa(PersonasEventos.ERROR_PERSONA_YA_EXISTE);
                    }

                    _logger.Informativo($"Obtener codigo persona...");

                    codPersona = await _personaRepository.ObtenerCodigoPersonaMax();
                    codPersona += 1;

                    _logger.Informativo($"Codigo persona obtenido");

                    if (dto.codigoTipoIdentificacion == 1) // Cedula
                    {
                        if (!dto.numeroIdentificacion.EsCedulaValida())
                            throw new ExcepcionOperativa(PersonasEventos.ERROR_CEDULA_INVALIDA);
                    }

                    _logger.Informativo($"Guardando persona...");


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.GuardarPersona(new
                            {
                                codigoPersona = codPersona,
                                dto.numeroIdentificacion,
                                fechaRegistro = DateTime.Now,
                                dto.observaciones,
                                dto.codigoTipoIdentificacion,
                                dto.codigoTipoPersona,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now,
                                fechaUsuarioRegistra = DateTime.Now,
                                dto.codigoTipoContribuyente,
                                dto.codigoAgencia,
                                _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            UniqueConstraint = () =>
                                throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_UNIQUE_CONSTRAINT),
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    _logger.Informativo($"Persona Guardada");

                    _logger.Informativo($"Guardando persona natural");


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaNaturalRepository.GuardarPersonaNatural(new
                            {
                                codigoPersona = codPersona,
                                dto.nombres,
                                dto.apellidoPaterno,
                                dto.apellidoMaterno,
                                dto.fechaNacimiento,
                                dto.tieneDiscapacidad,
                                dto.codigoTipoDiscapacidad,
                                dto.porcentajeDiscapacidad,
                                dto.codigoPaisNacimiento,
                                dto.codigoProvinciaNacimiento,
                                dto.codigoCiudadNacimiento,
                                dto.codigoParroquiaNacimiento,
                                dto.codigoTipoSangre,
                                dto.codigoConyuge,
                                dto.codigoEstadoCivil,
                                dto.codigoGenero,
                                dto.codigoProfesion,
                                dto.codigoTipoEtnia,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now,
                                vulnerable = dto.esVulnerable,
                            });
                        }, new DbExceptionEvents
                        {
                            UniqueConstraint = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos
                                    .PERSONA_NATURAL_ERROR_UNIQUE_CONSTRAINT),
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos.PERSONA_NATURAL_ERROR_FK),
                            CheckConstraint = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos
                                    .PERSONA_NATURAL_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // Colocacion de conyugue
                    if (dto.codigoEstadoCivil == 1 || dto.codigoEstadoCivil == 5)
                    {
                        if (dto.codigoConyuge != null)
                        {
                            try
                            {
                                await _dataBaseExceptions.CatchExceptionAsync(
                                    async () =>
                                    {
                                        await _personaNaturalRepository.ActualizarConyugue(new ActualizarConyugueRequest
                                        {
                                            codigoEstadoCivil = dto.codigoEstadoCivil,
                                            codigoConyuge = (long)dto.codigoConyuge,
                                            codigoPersona = codPersona
                                        });
                                    }, new DbExceptionEvents
                                    {
                                        UniqueConstraint = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos
                                                .PERSONA_NATURAL_ERROR_UNIQUE_CONSTRAINT),
                                        ForeignKeyViolation = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos.PERSONA_NATURAL_ERROR_FK),
                                        CheckConstraint = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos
                                                .PERSONA_NATURAL_ERROR_CHECK_CONSTRAINT),
                                        Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                                    });
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"ErrorActualizarConyugue=> {ex.InnerException}");
                            }
                        }
                    }

                    _logger.Informativo($"Persona natural guardada");

                    await _auditoria.AuditarAsync("PERS_PERSONAS_NATURALES", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarPersonaNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarPersonaNatural=> {ex}");
                        codigoEvento = PersonasNaturalesEventos.PERSONA_NATURAL_NO_GUARDADO;
                    }
                }

                _logger.Informativo($"GuardarPersonaNatural => {codigoEvento}");

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = codPersona
                };
            }
        }

        public async Task<Respuesta> ActualizarPersonaNatural(ActualizarPersonaNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = PersonasNaturalesEventos.ACTUALIZAR_PERSONA_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Actualizando persona natural...");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _personaNaturalRepository.ActualizarPersonaNatural(dto); },
                        new DbExceptionEvents
                        {
                            UniqueConstraint = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos
                                    .PERSONA_NATURAL_ERROR_UNIQUE_CONSTRAINT),
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos.PERSONA_NATURAL_ERROR_FK),
                            CheckConstraint = () =>
                                throw new ExcepcionOperativa(PersonasNaturalesEventos
                                    .PERSONA_NATURAL_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        throw new ExcepcionOperativa(PersonasNaturalesEventos.PERSONA_NATURAL_NO_ACTUALIZADO);
                    }

                    // Colocacion de conyugue
                    if (dto.codigoEstadoCivil == 1 || dto.codigoEstadoCivil == 5)
                    {
                        if (dto.codigoConyuge != null)
                        {
                            try
                            {
                                await _dataBaseExceptions.CatchExceptionAsync(
                                    async () =>
                                    {
                                        await _personaNaturalRepository.ActualizarConyugue(new ActualizarConyugueRequest
                                        {
                                            codigoEstadoCivil = dto.codigoEstadoCivil,
                                            codigoConyuge = (long)dto.codigoConyuge,
                                            codigoPersona = dto.codigoPersona
                                        });
                                    }, new DbExceptionEvents
                                    {
                                        UniqueConstraint = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos
                                                .PERSONA_NATURAL_ERROR_UNIQUE_CONSTRAINT),
                                        ForeignKeyViolation = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos.PERSONA_NATURAL_ERROR_FK),
                                        CheckConstraint = () =>
                                            throw new ExcepcionOperativa(PersonasNaturalesEventos
                                                .PERSONA_NATURAL_ERROR_CHECK_CONSTRAINT),
                                        Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                                    });
                            }

                            catch (Exception ex)
                            {
                                _logger.Error($"ErrorActualizarConyugue=> {ex.InnerException}");
                            }
                        }
                    }

                    await _auditoria.AuditarAsync("PERS_PERSONAS_NATURALES", dto);

                    scope.Complete();

                    _logger.Informativo($"Persona natural actualizada");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarPersonaNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarPersonaNatural=> {ex}");
                        codigoEvento = PersonasNaturalesEventos.PERSONA_NATURAL_NO_ACTUALIZADO;
                    }
                }

                _logger.Informativo($"ActualizarPersonaNatural => {codigoEvento}");

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null
                };
            }
        }

        public async Task<Respuesta> ObtenerInfoPesona(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                InfoPersonaNaturalDto persona = null;
                string codigoEvento = PersonasNaturalesEventos.OBTENER_INFORMACION_PERSONA;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Obteniendo información persona natural...");

                    persona = await _personaNaturalRepository.ObtenerInfoPersona(codigoPersona);
                    scope.Complete();

                    _logger.Informativo($"Información persona natural obtenida");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;


                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerInfoPesona => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerInfoPesona=> {ex}");
                        codigoEvento = PersonasNaturalesEventos.INFORMACION_PERSONA_NO_OBTENIDO;
                    }
                }

                _logger.Informativo($"ObtenerInfoPesona => {codigoEvento}");

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = persona
                };
            }
        }

        public async Task<Respuesta> ObtenerPersonaNatural(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                PersonaNatural persona = null;
                string codigoEvento = PersonasNaturalesEventos.OBTENER_PERSONA_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Obteniendo persona natural...");

                    persona = await _personaNaturalRepository.ObtenerPersonaNatural(codigoPersona);
                    scope.Complete();

                    _logger.Informativo($"Persona natural obtenida");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonaNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonaNatural=> {ex}");
                        codigoEvento = PersonasNaturalesEventos.PERSONAS_NATURALES_NO_OBTENIDOS;
                    }
                }

                _logger.Informativo($"ObtenerPersonaNatural => {codigoEvento}");

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = persona
                };
            }
        }
    }
}