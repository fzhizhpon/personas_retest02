using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.ReferenciasComerciales;
using Personas.Core.Entities.ReferenciasComerciales;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class ReferenciasComercialesService : IReferenciasComercialesService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<ReferenciasComercialesService> _logger;
        protected readonly IReferenciasComercialesRepository _referenciasComercialesRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public ReferenciasComercialesService(
            ConfiguracionApp config,
            IReferenciasComercialesRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<ReferenciasComercialesService> logger,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _config = config;
            _logger = logger;
            _textoInfoService = textoInfoService;
            _referenciasComercialesRepository = repository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarReferenciaComercial(GuardarReferenciaComercialDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = ReferenciasComercialesEventos.GUARDAR_REFERENCIA_COMERCIAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Guardando referencia comercial...");

                    dto.numeroRegistro =
                        await _referenciasComercialesRepository.ObtenerCodigoReferenciaComercial(dto.codigoPersona);

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasComercialesRepository.GuardarReferenciaComercial(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_FK),
                            CheckConstraint= () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_CHECK_CONSTRAINT),
                            
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_GUARDADO;
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

                    _logger.Informativo($"Referencia comercial guardada");

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_COMERCIALES", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarReferenciaComercial => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarReferenciaComercial => {exc}");
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_GUARDADO;
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

        public async Task<Respuesta> ObtenerReferenciaComercial(ObtenerReferenciaComercialDto dto)
        {
            ReferenciaComercial refComercial = null;
            string codigoEvento = ReferenciasComercialesEventos.OBTENER_REFERENCIA_COMERCIAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando referencia comercial...");

                    refComercial = await _referenciasComercialesRepository.ObtenerReferenciaComercial(dto);
                    scope.Complete();

                    if (refComercial == null)
                    {
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Referencia comercial consultada");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciaComercial => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciaComercial => {exc}");
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = refComercial
            };
        }

        public async Task<Respuesta> ObtenerReferenciasComerciales(ObtenerReferenciasComercialesDto dto)
        {
            IList<ReferenciaComercial.ReferenciaComercialMinimo> refComerciales =
                new List<ReferenciaComercial.ReferenciaComercialMinimo>();
            string codigoEvento = ReferenciasComercialesEventos.OBTENER_REFERENCIAS_COMERCIALES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando referencias comerciales...");

                    refComerciales = await _referenciasComercialesRepository.ObtenerReferenciasComerciales(dto);
                    scope.Complete();

                    if (refComerciales == null)
                    {
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIAS_COMERCIALES_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Referencias comerciales consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerReferenciasComerciales => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerReferenciasComerciales => {exc}");
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIAS_COMERCIALES_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = refComerciales
            };
        }

        public async Task<Respuesta> EliminarReferenciaComercial(EliminarReferenciaComercialDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = ReferenciasComercialesEventos.ELIMINAR_REFERENCIA_COMERCIAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Eliminar referencia comercial...");

                    int result = 0;


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasComercialesRepository.EliminarReferenciaComercial(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_FK),
                            CheckConstraint= () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_CHECK_CONSTRAINT),
                            
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else if (result > 1)
                    {
                        _logger.Warning($"Referencia comercial eliminado con {result} registros afectados!");
                    }
                    else
                    {
                        _logger.Informativo($"Referencia comercial eliminada");
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
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_COMERCIALES", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarReferenciaComercial => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarReferenciaComercial => {exc}");
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_ELIMINADO;
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

        public async Task<Respuesta> ActualizarReferenciaComercial(ActualizarReferenciaComercialDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = ReferenciasComercialesEventos.ACTUALIZAR_REFERENCIA_COMERCIAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Actualizando referencia comercial...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _referenciasComercialesRepository.ActualizarReferenciaComercial(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_FK),
                            CheckConstraint= () => throw new ExcepcionOperativa(ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_ERROR_CHECK_CONSTRAINT),
                            
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_ACTUALIZADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else if (result > 1)
                    {
                        _logger.Warning($"Referencia comercial actualizada con {result} registros afectados!");
                    }
                    else
                    {
                        _logger.Informativo($"Referencia comercial actualizada");
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
                    await _auditoria.AuditarAsync("PERS_REFERENCIAS_COMERCIALES", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarReferenciaComercial => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarReferenciaComercial => {exc}");
                        codigoEvento = ReferenciasComercialesEventos.REFERENCIA_COMERCIAL_NO_ACTUALIZADO;
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