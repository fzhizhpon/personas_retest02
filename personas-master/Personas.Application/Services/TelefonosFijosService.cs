using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.TelefonosFijos;
using Personas.Core.Entities.TelefonosFijos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class TelefonosFijosService : ITelefonosFijosService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<TelefonosFijosService> _logger;
        protected readonly ITelefonosFijosRepository _teleFijoRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;


        public TelefonosFijosService(
            ConfiguracionApp config,
            ILogsRepository<TelefonosFijosService> logger,
            ITelefonosFijosRepository teleFijoRepository,
            IMensajesRespuestaRepository textoInfoService,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _teleFijoRepository = teleFijoRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarTelefonoFijo(GuardarTelefonoFijoDto dto)
        {
            string codigoEvento = TelefonosFijosEventos.GUARDAR_TELEFONO_FIJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    int ultimoRegistro = await _teleFijoRepository.GenerarNumeroRegistro(dto.codigoPersona);
                    dto.numeroRegistro = ultimoRegistro + 1;

                    _logger.Informativo($"Guardando telefono fijo...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _teleFijoRepository.GuardarTelefonoFijo(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TelefonosFijosEventos.TELEFONO_FIJO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_GUARDADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_TELEFONOS_FIJO", dto);

                    scope.Complete();

                    _logger.Informativo($"Telefono fijo guardado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarTelefonoFijo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarTelefonoFijo => {exc}");
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarTelefonoFijo(ActualizarTelefonoFijoDto dto)
        {
            string codigoEvento = TelefonosFijosEventos.ACTUALIZAR_TELEFONO_FIJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;
                    _logger.Informativo($"Actualizando telefono fijo...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _teleFijoRepository.ActualizarTelefonoFijo(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TelefonosFijosEventos.TELEFONO_FIJO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_ACTUALIZADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_TELEFONOS_FIJO", dto);

                    scope.Complete();

                    _logger.Informativo($"Telefono fijo actualizado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarTelefonoFijo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarTelefonoFijo => {exc}");
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_ACTUALIZADO;
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

        public async Task<Respuesta> EliminarTelefonoFijo(EliminarTelefonoFijoDto dto)
        {
            string codigoEvento = TelefonosFijosEventos.ELIMINAR_TELEFONO_FIJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;
                    _logger.Informativo($"Eliminando telefono fijo...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _teleFijoRepository.EliminarTelefonoFijo(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TelefonosFijosEventos.TELEFONO_FIJO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_TELEFONOS_FIJO", dto);

                    scope.Complete();

                    _logger.Informativo($"Telefono fijo eliminado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarTelefonoFijo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarTelefonoFijo => {exc}");
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_ELIMINADO;
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

        public async Task<Respuesta> ObtenerTelefonosFijos(ObtenerTelefonosFijosDto dto)
        {
            IList<TelefonoFijo.TelefonoFijoMinimo> telefonosFijos = null;
            string codigoEvento = TelefonosFijosEventos.OBTENER_TELEFONOS_FIJOS;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando telefonos fijos...");

                    telefonosFijos = await _teleFijoRepository.ObtenerTelefonosFijos(dto);

                    if (telefonosFijos == null || telefonosFijos.Count == 0)
                    {
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJOS_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Telefonos fijos consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerTelefonosFijos => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerTelefonosFijos => {exc}");
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJOS_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = telefonosFijos
            };
        }

        public async Task<Respuesta> ObtenerTelefonoFijo(ObtenerTelefonoFijoDto dto)
        {
            TelefonoFijo.TelefonoFijoFull telefonoFijo = null;
            string codigoEvento = TelefonosFijosEventos.OBTENER_TELEFONO_FIJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando telefono fijo...");

                    telefonoFijo = await _teleFijoRepository.ObtenerTelefonoFijo(dto);

                    if (telefonoFijo == null)
                    {
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Telefono fijo consultado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerTelefonoFijo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerTelefonoFijo => {exc}");
                        codigoEvento = TelefonosFijosEventos.TELEFONOS_FIJO_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = telefonoFijo
            };
        }
    }
}