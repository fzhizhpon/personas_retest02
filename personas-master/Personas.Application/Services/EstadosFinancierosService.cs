using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.EstadosFinancieros;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.EstadosFinancieros;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Excepciones;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;

namespace Personas.Application.Services
{
    public class EstadosFinancierosService : IEstadosFinancierosService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<EstadosFinancierosService> _logger;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly IEstadosFinancierosRepository _estadoFinRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public EstadosFinancierosService(
            ConfiguracionApp config,
            ILogsRepository<EstadosFinancierosService> logger,
            IMensajesRespuestaRepository textoInfoService,
            IEstadosFinancierosRepository estadoFinRepository,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _estadoFinRepository = estadoFinRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarEstadosFinancieros(GuardarEstadoFinancieroDto dto)
        {

            string codigoEvento = EstadosFinancierosEventos.GUARDAR_ESTADO_FINANCIERO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Guardando estado financiero...");

                    dto.fechaUsuarioActualiza = DateTime.Now;
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _estadoFinRepository.GuardarEstadosFinancieros(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(EstadosFinancierosEventos.ESTADO_FINANCIERO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = EstadosFinancierosEventos.ESTADO_FINANCIERO_NO_GUARDADO;
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
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_ESTADOS_FINANCIEROS", dto);

                    scope.Complete();

                    _logger.Informativo($"Estado financiero guardado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarEstadosFinancieros => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarEstadosFinancieros => {exc}");
                        codigoEvento = EstadosFinancierosEventos.ESTADO_FINANCIERO_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarEstadosFinancieros(ActualizarEstadoFinancieroDto dto)
        {
            string codigoEvento = EstadosFinancierosEventos.ACTUALIZAR_ESTADO_FINANCIERO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Actualizando estado financiero...");

                    dto.fechaUsuarioActualiza = DateTime.Now;
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _estadoFinRepository.ActualizarEstadosFinancieros(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(EstadosFinancierosEventos.ESTADO_FINANCIERO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


                    if (result == 0)
                    {
                        codigoEvento = EstadosFinancierosEventos.ESTADO_FINANCIERO_NO_ACTUALIZADO;
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
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_ESTADOS_FINANCIEROS", dto);

                    scope.Complete();

                    _logger.Informativo($"Estado financiero actualizado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarEstadosFinancieros => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarEstadosFinancieros => {exc}");
                        codigoEvento = EstadosFinancierosEventos.ESTADO_FINANCIERO_NO_ACTUALIZADO;
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

        public async Task<Respuesta> ObtenerCuentasEstadosFinancieros(ObtenerCuentasEstadoFinancieroDto dto)
        {
            List<EstadoFinanciero> cuentasEstFin = null;
            string codigoEvento = EstadosFinancierosEventos.OBTENER_ESTADOS_FINANCIEROS;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando cuentas estado financiero...");

                    cuentasEstFin = await _estadoFinRepository.ObtenerCuentasEstadosFinancieros(dto);

                    cuentasEstFin.ForEach(async cuenta =>
                    {
                        if (!String.IsNullOrEmpty(cuenta.query))
                        {
                            cuenta.valor =
                                await _estadoFinRepository.ObtenerValorCuentaPorQuery(cuenta.query, dto.codigoPersona);
                            cuenta.recursoExterno = true;
                        }
                    });

                    if (cuentasEstFin == null)
                    {
                        codigoEvento = EstadosFinancierosEventos.ESTADOS_FINANCIEROS_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Cuentas estado financiero consultadas");

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerCuentasEstadosFinancieros => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerCuentasEstadosFinancieros => {exc}");
                        codigoEvento = EstadosFinancierosEventos.ESTADOS_FINANCIEROS_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = cuentasEstFin
            };
        }
    }
}