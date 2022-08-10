using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.ReferenciasFinancieras;
using Personas.Core.Entities.ReferenciasFinancieras;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class ReferenciasFinancierasService : IReferenciasFinancierasService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<ReferenciasFinancierasService> _logger;
        protected readonly IReferenciasFinancierasRepository _referenciasFinancierasRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public ReferenciasFinancierasService(
            ConfiguracionApp config,
            ILogsRepository<ReferenciasFinancierasService> logger,
            IMensajesRespuestaRepository textoInfoService,
            IReferenciasFinancierasRepository serviceReferenciasFinancierasRepository,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _referenciasFinancierasRepository = serviceReferenciasFinancierasRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarReferenciaFinanciera(GuardarReferenciaFinancieraDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = ReferenciasFinancierasEventos.GUARDAR_REFERENCIA_FINANCIERA;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Guardando referencia financiera...");

                    int codigoReferencia =
                        await _referenciasFinancierasRepository.ObtenerCodigoReferenciaFinanciera(dto.codigoPersona);
                    dto.numeroRegistro = codigoReferencia;

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasFinancierasRepository.GuardarReferenciaFinanciera(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(ReferenciasFinancierasEventos
                                    .REFERENCIA_FINANCIERA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_GUARDADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                    {
                        codigoPersona = dto.codigoPersona,
                        fechaUsuarioActualiza = DateTime.Now,
                        codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                    });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_FINANCIERAS", dto);

                    scope.Complete();

                    _logger.Informativo($"Referencia financiera guardada");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarReferenciaFinanciera => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarReferenciaFinanciera => {exc}");
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_GUARDADO;
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

        public async Task<Respuesta> ObtenerReferenciaFinanciera(ObtenerReferenciaFinancieraDto dto)
        {
            ReferenciaFinanciera refFinanciera = null;
            string codigoEvento = ReferenciasFinancierasEventos.OBTENER_REFERENCIA_FINANCIERA;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando referencia financiera...");

                    refFinanciera = await _referenciasFinancierasRepository.ObtenerReferenciaFinanciera(dto);

                    if (refFinanciera == null)

                    {
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Referencia financiera no consultada");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciaFinanciera => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciaFinanciera => {exc}");
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = refFinanciera
            };
        }

        public async Task<Respuesta> ObtenerReferenciasFinancieras(ObtenerReferenciasFinancierasDto dto)
        {
            IList<ReferenciaFinanciera> refFinancieras = new List<ReferenciaFinanciera>();
            string codigoEvento = ReferenciasFinancierasEventos.OBTENER_REFERENCIAS_FINANCIERAS;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando varias referencias financieras...");

                    refFinancieras = await _referenciasFinancierasRepository.ObtenerReferenciasFinancieras(dto);
                    scope.Complete();

                    if (refFinancieras == null)
                    {
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIAS_FINANCIERAS_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Varias referencias financieras consultadas");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciasFinancieras => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciasFinancieras => {exc}");
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIAS_FINANCIERAS_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = refFinancieras
            };
        }

        public async Task<Respuesta> EliminarReferenciaFinanciera(EliminarReferenciaFinancieraDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = ReferenciasFinancierasEventos.ELIMINAR_REFERENCIA_FINANCIERA;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    // Consulto referencia para guardar en historico
                    ReferenciaFinanciera refFinanciera = await _referenciasFinancierasRepository
                        .ObtenerReferenciaFinanciera(new ObtenerReferenciaFinancieraDto()
                        {
                            codigoPersona = dto.codigoPersona,
                            numeroRegistro = dto.numeroRegistro
                        });

                    _logger.Informativo($"Eliminar trabajo...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasFinancierasRepository.EliminarReferenciaFinanciera(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(ReferenciasFinancierasEventos
                                    .REFERENCIA_FINANCIERA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else
                    {
                        refFinanciera.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;
                        refFinanciera.fechaUsuarioRegistra = DateTime.Now;
                        refFinanciera.guid = _config.guid;

                        if (result > 1)
                        {
                            _logger.Warning($"Referencia financiera eliminada con {result} registros afectados!");
                        }
                        else
                        {
                            _logger.Informativo($"Referencia financiera eliminada");
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
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_FINANCIERAS", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarReferenciaFinanciera => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarReferenciaFinanciera => {exc}");
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_ELIMINADO;
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

        public async Task<Respuesta> ActualizarReferenciaFinanciera(ActualizarReferenciaFinancieraDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = ReferenciasFinancierasEventos.ACTUALIZAR_REFERENCIA_FINANCIERA;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    // Consulto referencia para guardar en historico
                    ReferenciaFinanciera refFinanciera = await _referenciasFinancierasRepository
                        .ObtenerReferenciaFinanciera(new ObtenerReferenciaFinancieraDto()
                        {
                            codigoPersona = dto.codigoPersona,
                            numeroRegistro = dto.numeroRegistro
                        });

                    _logger.Informativo($"Actualizando referencia financiera...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasFinancierasRepository.ActualizarReferenciaFinanciera(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(ReferenciasFinancierasEventos
                                    .REFERENCIA_FINANCIERA_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_ACTUALIZADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else
                    {
                        refFinanciera.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;
                        refFinanciera.fechaUsuarioRegistra = DateTime.Now;
                        refFinanciera.guid = _config.guid;

                        if (result > 1)
                        {
                            _logger.Warning($"Referencia financiera actualizada con {result} registros afectados!");
                        }
                        else
                        {
                            _logger.Informativo($"Referencia financiera actualizada");
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
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_FINANCIERAS", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarReferenciaFinanciera => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarReferenciaFinanciera => {exc}");
                        codigoEvento = ReferenciasFinancierasEventos.REFERENCIA_FINANCIERA_NO_ACTUALIZADO;
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