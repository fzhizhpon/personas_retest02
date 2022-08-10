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

namespace Personas.Application.Services
{
    public class PersonaNoNaturalService : IPersonaNoNaturalService
    {
        protected readonly IPersonaRepository _personaRepository;
        protected readonly IPersonaNoNaturalRepository _personaNoNaturalRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<PersonaNoNaturalService> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public PersonaNoNaturalService(IPersonaNoNaturalRepository repository,
            IPersonaRepository personaRepository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<PersonaNoNaturalService> logger,
            ConfiguracionApp config,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _personaNoNaturalRepository = repository;
            _personaRepository = personaRepository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarPersonaNoNatural(GuardarPersonaNoNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = PersonasNoNaturalesEventos.GUARDAR_PERSONA_NO_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
                long codPersona = 0;

                try
                {
                    int nroPersonas =
                        await _personaRepository.ObtenerPersonasPorIdentificacion(dto.numeroIdentificacion);


                    if (nroPersonas != 0)
                    {
                        throw new ExcepcionOperativa(PersonasEventos.ERROR_PERSONA_YA_EXISTE);
                    }

                    dto.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioRegistra = DateTime.Now;

                    _logger.Informativo($"Obtener codigo persona...");

                    codPersona = await _personaRepository.ObtenerCodigoPersonaMax();
                    codPersona += 1;

                    _logger.Informativo($"Codigo persona obtenido");

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
                                _config.codigoUsuarioRegistra,
                                fechaUsuarioRegistra = DateTime.Now,
                                dto.codigoTipoContribuyente,
                                dto.codigoAgencia
                            });
                        }, new DbExceptionEvents
                        {
                            UniqueConstraint = () =>
                                throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_UNIQUE_CONSTRAINT),
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo($"Persona Guardada");

                    _logger.Informativo($"Guardando persona no natural");
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaNoNaturalRepository.GuardarPersonaNoNatural(new
                            {
                                codigoPersona = codPersona,
                                dto.razonSocial,
                                dto.fechaConstitucion,
                                dto.objetoSocial,
                                dto.tipoSociedad,
                                dto.finalidadLucro,
                                dto.obligadoLlevarContabilidad,
                                dto.agenteRetencion,
                                dto.direccionWeb,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now
                            });
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_PERSONAS_NO_NATURALES", dto);

                    scope.Complete();

                    _logger.Informativo($"Persona no natural guardada");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarPersonaNoNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarPersonaNoNatural=> {ex}");
                        codigoEvento = PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_NO_GUARDADO;
                    }
                }

                _logger.Informativo($"GuardarPersonaNoNatural => {codigoEvento}");

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

        public async Task<Respuesta> ObtenerPersonaNoNatural(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                PersonaNoNatural persona = null;
                string codigoEvento = PersonasNoNaturalesEventos.OBTENER_PERSONA_NO_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Obteniendo persona no natural...");

                    persona = await _personaNoNaturalRepository.ObtenerPersonaNoNatural(codigoPersona);
                    scope.Complete();

                    _logger.Informativo($"Persona no natural obtenida");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonaNoNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonaNoNatural=> {ex}");
                        codigoEvento = PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_NO_OBTENIDO;
                    }
                }

                _logger.Informativo($"ObtenerPersonaNoNatural => {codigoEvento}");

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

        public async Task<Respuesta> ActualizarPersonaNoNatural(ActualizarPersonaNoNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = PersonasNoNaturalesEventos.ACTUALIZAR_PERSONA_NO_NATURAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Actualizando persona no natural...");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _personaNoNaturalRepository.ActualizarPersonaNoNatural(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        throw new ExcepcionOperativa(PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_NO_ACTUALIZADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_PERSONAS_NO_NATURALES", dto);

                    scope.Complete();

                    _logger.Informativo($"Persona no natural actualizada");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarPersonaNoNatural => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarPersonaNoNatural=> {ex}");
                        codigoEvento = PersonasNoNaturalesEventos.PERSONA_NO_NATURAL_NO_ACTUALIZADO;
                    }
                }

                _logger.Informativo($"ActualizarPersonaNoNatural => {codigoEvento}");

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
    }
}