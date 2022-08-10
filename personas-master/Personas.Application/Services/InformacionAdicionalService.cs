using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.TablasComunes;
using Personas.Core.Entities.TablasComunes;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class InformacionAdicionalService : IInformacionAdicionalService
    {
        private readonly IInformacionAdicionalRepository _tablasComunesDetallesRepository;
        private readonly ConfiguracionApp _config; // configuracion
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<InformacionAdicional> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public InformacionAdicionalService(
            IInformacionAdicionalRepository tablasComunesDetallesRepository,
            ConfiguracionApp config,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<InformacionAdicional> logger,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _tablasComunesDetallesRepository = tablasComunesDetallesRepository;
            _config = config;
            _textoInfoService = textoInfoService;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ObtenerInformacionAdicional(long codigoReferencia, long codigoTabla)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            IEnumerable<InformacionAdicional> tablasComunesDetalles = null;
            string codigoEvento = InformacionAdicionalEventos.OBTENER_INFORMACION_ADICIONAL;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo InformacionAdicional");
                    (codigo, tablasComunesDetalles) =
                        await _tablasComunesDetallesRepository.ObtenerInformacionAdicional(codigoReferencia,
                            codigoTabla);
                    _logger.Informativo("Obtenido InformacionAdicional");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerInformacionAdicional => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerInformacionAdicional=> {ex}");
                        codigoEvento = InformacionAdicionalEventos.INFORMACION_ADICIONAL_NO_OBTENIDOS;
                    }
                }

                string textoInfo =
                    await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = tablasComunesDetalles
                };
            }
        }

        public async Task<Respuesta> GuardarInformacionAdicional(GuardarInformacionAdicionalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = InformacionAdicionalEventos.GUARDAR_INFORMACION_ADICIONAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Guardando InformacionAdicional...");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            await _tablasComunesDetallesRepository.GuardarInformacionAdicional(
                                new GuardarInformacionAdicionalDto()
                                {
                                    codigoReferencia = dto.codigoReferencia,
                                    codigoTabla = dto.codigoTabla,
                                    codigoElemento = dto.codigoElemento,
                                    observacion = dto.observacion,
                                    estado = '1',
                                    codigoModulo = dto.codigoModulo
                                });
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(InformacionAdicionalEventos.INFORMACION_ADICIONAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Guardado InformacionAdicional...");

                    // * auditoria
                    await _auditoria.AuditarAsync("SIFV_DETALLE_INF_ADIC", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarInformacionAdicional => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarInformacionAdicional=> {ex}");
                        codigoEvento = InformacionAdicionalEventos.INFORMACION_ADICIONAL_NO_GUARDADO;
                    }
                }

                string textoInfo =
                    await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null
                };
            }
        }

        public async Task<Respuesta> ActualizarInformacionAdicional(ActualizarInformacionAdicionalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = InformacionAdicionalEventos.ACTUALIZAR_INFORMACION_ADICIONAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Actualizando InformacionAdicional");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    int resultado = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            resultado = await _tablasComunesDetallesRepository.ActualizarInformacionAdicional(
                                new ActualizarInformacionAdicionalDto()
                                {
                                    codigoReferencia = dto.codigoReferencia,
                                    codigoTabla = dto.codigoTabla,
                                    codigoElemento = dto.codigoElemento,
                                    observacion = dto.observacion,
                                    estado = dto.estado,
                                    codigoModulo = dto.codigoModulo
                                });
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(InformacionAdicionalEventos.INFORMACION_ADICIONAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Actualizo InformacionAdicional");

                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(InformacionAdicionalEventos.INFORMACION_ADICIONAL_NO_ACTUALIZADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("SIFV_DETALLE_INF_ADIC", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarOpcion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarInformacionAdicional=> {ex}");
                        codigoEvento = InformacionAdicionalEventos.INFORMACION_ADICIONAL_NO_ACTUALIZADO;
                    }
                }

                string textoInfo =
                    await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

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