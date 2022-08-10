using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.ReferenciasPersonales;
using Personas.Core.Entities.ReferenciasPersonales;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class ReferenciasPersonalesService : IReferenciasPersonalesService
    {
        private readonly ConfiguracionApp _config;
        private readonly IMensajesRespuestaRepository _textoInfoService;
        private readonly ILogsRepository<ReferenciasPersonalesService> _logger;
        private readonly IReferenciasPersonalesRepository _referenciasPersonalesRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;


        public ReferenciasPersonalesService(
            ConfiguracionApp config,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<ReferenciasPersonalesService> logger,
            IReferenciasPersonalesRepository serviceReferenciasPersonalesRepository,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _config = config;
            _logger = logger;
            _textoInfoService = textoInfoService;
            _referenciasPersonalesRepository = serviceReferenciasPersonalesRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarReferenciaPersonal(GuardarReferenciaPersonalDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
            string codigoEvento = ReferenciasPersonalesEventos.GUARDAR_REFERENCIA_PERSONAL;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Existe referencia personal...");
                    var existeReferencia = await _referenciasPersonalesRepository.ObtenerReferenciaPersonal(
                        new ObtenerReferenciaPersonalDto()
                        {
                            codigoPersona = dto.codigoPersona,
                            codigoPersonaReferida = dto.codigoPersonaReferida
                        });

                    if (existeReferencia is not null)
                    {
                        throw new ExcepcionOperativa(ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_ERROR_FK);
                    }

                    _logger.Informativo($"Existio referencia personal...");


                    _logger.Informativo($"Verificando referencia personal...");

                    var resultadoVerificacion =
                        await _referenciasPersonalesRepository.esUnaReferenciaPersonalEliminada(
                            dto.codigoPersonaReferida, dto.codigoPersona);

                    _logger.Informativo($"Verificado referencia personal...");


                    if (resultadoVerificacion is not null)
                    {
                        // * si existe previamente la referencia personal ingresada se lo va a volver a reactivar y modificar los campos
                        _logger.Informativo($"Reactivando referencia personal...");

                        int result = 0;

                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _referenciasPersonalesRepository.reactivarReferenciaPersonal(
                                    new ActualizarReferenciaPersonalDto()
                                    {
                                        codigoPersona = dto.codigoPersona,
                                        codigoPersonaReferida = dto.codigoPersonaReferida,
                                        codigoUsuarioActualiza = dto.codigoUsuarioActualiza,
                                        fechaConoce = dto.FechaConoce,
                                        fechaUsuarioActualiza = dto.fechaUsuarioActualiza,
                                        observaciones = dto.Observaciones
                                    });
                            },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos
                                        .REFERENCIA_PERSONAL_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });

                        if (result == 0)
                        {
                            codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_GUARDADO;
                            codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                            throw new ExcepcionOperativa(codigoEvento);
                        }

                        _logger.Informativo($"Reactivada referencia personal...");
                    }
                    else
                    {
                        // * no existe previamente la referencia personal ingresada se procede a guardar
                        _logger.Informativo($"Guardando referencia personal...");
                        dto.numeroRegistro =
                            await _referenciasPersonalesRepository.ObtenerCodigoReferenciaFinanciera(dto.codigoPersona);

                        int result = 0;


                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _referenciasPersonalesRepository.GuardarReferenciaPersonal(dto);
                            },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos
                                        .REFERENCIA_PERSONAL_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });

                        if (result == 0)
                        {
                            codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_GUARDADO;
                            codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                            throw new ExcepcionOperativa(codigoEvento);
                        }

                        _logger.Informativo($"Referencia personal guardada");
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                            {
                                codigoPersona = dto.codigoPersona,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_PERSONALES", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarReferenciaPersonal => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarReferenciaPersonal => {exc}");
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_GUARDADO;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = null
            };
        }

        public async Task<Respuesta> ObtenerReferenciaPersonal(ObtenerReferenciaPersonalDto dto)
        {
            ReferenciaPersonal.ReferenciaPersonalJoin refPersonal = null;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
            string codigoEvento = ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando referencia personal...");

                    refPersonal = await _referenciasPersonalesRepository.ObtenerReferenciaPersonalJoin(dto);
                    scope.Complete();

                    if (refPersonal == null)
                    {
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Referencia personal consultada");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciaPersonalJoin => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciaPersonalJoin => {exc}");
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = refPersonal
            };
        }

        public async Task<Respuesta> ObtenerReferenciasPersonales(ObtenerReferenciasPersonalesDto dto)
        {
            IList<ReferenciaPersonal.ReferenciaPersonalMinimo> refPersonales =
                new List<ReferenciaPersonal.ReferenciaPersonalMinimo>();
            string codigoEvento = ReferenciasPersonalesEventos.OBTENER_REFERENCIAS_PERSONALES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando referencias personales...");

                    refPersonales = await _referenciasPersonalesRepository.ObtenerReferenciasPersonales(dto);
                    scope.Complete();

                    if (refPersonales == null)
                    {
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIAS_PERSONALES_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Referencias personales consultadas");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciasPersonales => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciasPersonales => {exc}");
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIAS_PERSONALES_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = refPersonales
            };
        }

        public async Task<Respuesta> EliminarReferenciaPersonal(EliminarReferenciaPersonalDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = ReferenciasPersonalesEventos.ELIMINAR_REFERENCIA_PERSONAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    // Obteniendo referencia personal para historico
                    ReferenciaPersonal refPersonal = await _referenciasPersonalesRepository.ObtenerReferenciaPersonal(
                        new ObtenerReferenciaPersonalDto()
                        {
                            codigoPersona = dto.codigoPersona,
                            codigoPersonaReferida = dto.codigoPersonaReferida
                        });

                    if (refPersonal == null)
                    {
                        _logger.Informativo($"Referencia personal no existe o estado 0");
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Eliminando referencia personal...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasPersonalesRepository.EliminarReferenciaPersonal(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(ReferenciasPersonalesEventos
                                    .REFERENCIA_PERSONAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else
                    {
                        refPersonal.codigoUsuarioRegistro = _config.codigoUsuarioRegistra;
                        refPersonal.fechaUsuarioRegistro = DateTime.Now;
                        refPersonal.guid = _config.guid;

                        if (result > 1)
                        {
                            _logger.Warning($"Referencia personal eliminada con {result} registros afectados!");
                        }
                        else
                        {
                            _logger.Informativo($"Referencia personal eliminada");
                        }
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                            {
                                codigoPersona = dto.codigoPersona,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_PERSONALES", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarReferenciaPersonal => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarReferenciaPersonal => {exc}");
                        codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_ELIMINADO;
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
    }
}