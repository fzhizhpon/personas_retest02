using System;
using System.Collections.Generic;
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
    public class PersonaService : IPersonaService
    {
        protected readonly IPersonaRepository _personasRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<PersonaService> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public PersonaService(IPersonaRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<PersonaService> logger,
            ConfiguracionApp config,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _personasRepository = repository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ActualizarPersona(ActualizarPersonaDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = PersonasEventos.ACTUALIZAR_PERSONA;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Actualizando persona");

                    dto.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioRegistra = DateTime.Now;

                    if (dto.codigoTipoIdentificacion == 1) // Cedula
                    {
                        if (!dto.numeroIdentificacion.EsCedulaValida())
                            throw new ExcepcionOperativa(PersonasEventos.ERROR_CEDULA_INVALIDA);
                    }

                    if (dto.codigoTipoIdentificacion == 2) // RUC
                    {
                        if (!dto.numeroIdentificacion.EsRucValido())
                            throw new ExcepcionOperativa(PersonasEventos.ERROR_RUC_INVALIDO);
                    }


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _personasRepository.ActualizarPersona(dto); },
                        new DbExceptionEvents
                        {
                            UniqueConstraint = () =>
                                throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_UNIQUE_CONSTRAINT),
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(PersonasEventos.PERSONA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        throw new ExcepcionOperativa(PersonasEventos.PERSONA_NO_ACTUALIZADO);
                    }
                    
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => {

                    await _personasRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                    {
                        codigoPersona = dto.codigoPersona,
                        fechaUsuarioActualiza = dto.fechaUsuarioRegistra ?? DateTime.Now,
                        codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                    });
                    
                        }, new DbExceptionEvents {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });
                    
                    
                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_PERSONAS", dto);

                    scope.Complete();

                    _logger.Informativo($"Persona actualizada");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarPersona => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarPersona=> {ex}");
                        codigoEvento = PersonasEventos.PERSONA_NO_ACTUALIZADO;
                    }
                }

                _logger.Informativo($"ActualizarPersona => {codigoEvento}");

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

        public async Task<Respuesta> ObtenerPersona(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Persona persona = null;
                string codigoEvento = PersonasEventos.OBTENER_PERSONA;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Obteniendo persona");

                    persona = await _personasRepository.ObtenerPersona(codigoPersona);
                    scope.Complete();

                    _logger.Informativo($"Persona obtenida");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersona => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersona=> {ex}");
                        codigoEvento = PersonasEventos.PERSONA_NO_OBTENIDOS;
                    }
                }

                _logger.Informativo($"ObtenerPersona => {codigoEvento}");

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

        public async Task<Respuesta> ObtenerPersonaJoinMinimo(UltActPersonaRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string codigoEvento = PersonasEventos.OBTENER_PERSONA;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
                Persona.PersonaJoinMinimo persona = null;

                try
                {
                    persona = await _personasRepository.ObtenerPersonaJoinMinimo(dto);
                    _logger.Informativo($"ObtenerPersonaJoinMinimo => {codigoEvento}");
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonaJoinMinimo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonaJoinMinimo => {ex}");
                        codigoEvento = PersonasEventos.PERSONA_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

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

        public async Task<Respuesta> ObtenerPersonas(PersonaRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {

                List<PersonaResponse> personas = new List<PersonaResponse>();
                string codigoEvento = PersonasEventos.OBTENER_PERSONAS; // Se obtuvieron los correos
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


                try
                {
                    personas = _personasRepository.ObtenerPersonas(dto);
                    _logger.Informativo($"ObtenerPersonas => {codigoEvento}");
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonas => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonas => {ex}");
                        codigoEvento = PersonasEventos.PERSONAS_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = personas
                };
            }
        }
    }
}