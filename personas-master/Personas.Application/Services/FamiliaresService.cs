using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Familiares;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Familiares;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class FamiliaresService : IFamiliaresService
    {
        private readonly ConfiguracionApp _config;
        private readonly ILogsRepository<RepresentantesService> _logger;
        private readonly IMensajesRespuestaRepository _textoInfoService;

        private readonly IFamiliaresRepository _familiaresRepository;

        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public FamiliaresService(
            ConfiguracionApp config,
            ILogsRepository<RepresentantesService> logger,
            IMensajesRespuestaRepository textoInfoService,
            IFamiliaresRepository familiaresRepository,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _familiaresRepository = familiaresRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarFamiliar(GuardarFamiliarDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = FamiliaresEventos.GUARDAR_FAMILIAR;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    if (dto.codigoPersona == dto.codigoPersonaFamiliar)
                        throw new ExcepcionOperativa(FamiliaresEventos.CODIGO_PERSONA_IGUAL_FAMILIAR);

                    DateTime fechaActual = DateTime.Now;

                    Familiar familiar = new Familiar();

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            familiar = await _familiaresRepository.ObtenerFamiliar(new ObtenerFamiliarDto()
                            {
                                codigoPersona = dto.codigoPersona,
                                codigoPersonaFamiliar = dto.codigoPersonaFamiliar
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    int result = 0;

                    if (familiar != null)
                    {
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _familiaresRepository.ActualizarFamiliar(new ActualizarFamiliarDto()
                                {
                                    codigoPersonaFamiliar = dto.codigoPersonaFamiliar,
                                    codigoPersona = dto.codigoPersona,
                                    codigoParentesco = dto.codigoParentesco,
                                    esCargaFamiliar = dto.esCargaFamiliar,
                                    observacion = dto.observacion,
                                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                    fechaUsuarioActualiza = fechaActual,
                                });
                            }, new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_FK),
                                CheckConstraint = () =>
                                    throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_CHECK_CONSTRAINT),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }
                    else
                    {
                        _logger.Informativo($"Guardando familiar...");
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () => { result = await _familiaresRepository.GuardarFamiliar(dto); },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_FK),
                                CheckConstraint = () =>
                                    throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_CHECK_CONSTRAINT),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.ColocarFechaUltimaActualizacion(
                                new UltActPersonaRequest
                                {
                                    codigoPersona = dto.codigoPersona,
                                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                    fechaUsuarioActualiza = fechaActual
                                });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_GUARDADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_FAMILIARES", dto);

                    scope.Complete();

                    _logger.Informativo($"Familiar guardado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarFamiliar => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarFamiliar => {exc}");
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_GUARDADO;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = null
            };
        }

        public async Task<Respuesta> ActualizarFamiliar(ActualizarFamiliarDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = FamiliaresEventos.ACTUALIZAR_FAMILIAR;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    if (dto.codigoPersona == dto.codigoPersonaFamiliar)
                        throw new ExcepcionOperativa(FamiliaresEventos.CODIGO_PERSONA_IGUAL_FAMILIAR);

                    DateTime fechaActual = DateTime.Now;
                    dto.fechaUsuarioActualiza = fechaActual;
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

                    _logger.Informativo($"Actualizando familiar...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _familiaresRepository.ActualizarFamiliar(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_FK),
                            CheckConstraint = () =>
                                throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_ACTUALIZADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                            {
                                codigoPersona = dto.codigoPersona,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = fechaActual
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_FAMILIARES", dto);

                    scope.Complete();

                    _logger.Informativo($"Familiar actualizado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarFamiliar => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarFamiliar => {exc}");
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_ACTUALIZADO;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = null
            };
        }

        public async Task<Respuesta> ObtenerFamiliares(ObtenerFamiliaresDto dto)
        {
            IList<Familiar.FamiliarJoinMinimo> familiares = new List<Familiar.FamiliarJoinMinimo>();
            string codigoEvento = FamiliaresEventos.OBTENER_FAMILIARES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando familiares...");

                    familiares = await _familiaresRepository.ObtenerFamiliaresJoinMinimo(dto);

                    if (familiares == null)
                    {
                        codigoEvento = FamiliaresEventos.FAMILIARES_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Familiares consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerFamiliaresJoinMinimo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerFamiliaresJoinMinimo => {exc}");
                        codigoEvento = FamiliaresEventos.FAMILIARES_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = familiares
            };
        }

        public async Task<Respuesta> EliminarFamiliar(EliminarFamiliarDto dto)
        {
            string codigoEvento = FamiliaresEventos.ELIMINAR_FAMILIAR;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Eliminando familiar...");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _familiaresRepository.EliminarFamiliar(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(FamiliaresEventos.FAMILIARES_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                            {
                                codigoPersona = dto.codigoPersona,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_FAMILIARES", dto);

                    scope.Complete();

                    _logger.Informativo($"Representante eliminado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarFamiliar => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarFamiliar => {exc}");
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_ELIMINADO;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = null
            };
        }

        public async Task<Respuesta> ObtenerFamiliar(ObtenerFamiliarDto dto)
        {
            Familiar.FamiliarJoinFull familiar = null;
            string codigoEvento = FamiliaresEventos.OBTENER_FAMILIAR;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando familiar...");

                    familiar = await _familiaresRepository.ObtenerFamiliarJoinFull(dto);

                    if (familiar == null)
                    {
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Familiar consultado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerFamiliarJoinFull => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerFamiliarJoinFull => {exc}");
                        codigoEvento = FamiliaresEventos.FAMILIAR_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = familiar
            };
        }
    }
}