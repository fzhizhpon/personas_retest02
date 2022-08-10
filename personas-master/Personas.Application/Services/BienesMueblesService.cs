using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.BienesMuebles;
using Personas.Core.Entities.BienesMuebles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class BienesMueblesService : IBienesMueblesService
    {
        private readonly IBienesMueblesRepository _bienesMueblesRepository; // interfaz del repositorio correspondiente

        private readonly ConfiguracionApp _config; // configuracion
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<BienesMuebles> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public BienesMueblesService(
            IBienesMueblesRepository bienesMueblesRepository,
            ConfiguracionApp configuracionApp,
            ILogsRepository<BienesMuebles> logger,
            IMensajesRespuestaRepository textoInfoService,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _bienesMueblesRepository = bienesMueblesRepository;
            _config = configuracionApp;
            _textoInfoService = textoInfoService;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ObtenerBienesMuebles(long codigoPersona)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            IEnumerable<BienesMuebles> bienesMuebles = null;
            // * codigos de eventos
            string codigoEvento = BienesMueblesEventos.OBTENER_BIENES_MUEBLES;
            // * codigo interno de funcionamiento
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienesMuebles");
                    (codigo, bienesMuebles) = await _bienesMueblesRepository.ObtenerBienesMuebles(codigoPersona);
                    _logger.Informativo("Obtenido BienesMuebles");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienesMuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienesMuebles=> {ex}");
                        codigoEvento = BienesMueblesEventos.BIENES_MUEBLES_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienesMuebles
            };
        }

        public async Task<Respuesta> ObtenerBienMueble(long codigoPersona, long numeroRegistro)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            BienesMuebles bienMueble = null;
            // * codigos de eventos
            string codigoEvento = BienesMueblesEventos.OBTENER_BIEN_MUEBLE;
            // * codigo interno de funcionamiento
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienMueble");
                    (codigo, bienMueble) =
                        await _bienesMueblesRepository.ObtenerBienMueble(codigoPersona, numeroRegistro);
                    _logger.Informativo("Obtenido BienMueble");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienMueble => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienMueble=> {ex}");
                        codigoEvento = BienesMueblesEventos.BIEN_MUEBLE_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienMueble
            };
        }

        public async Task<Respuesta> GuardarBienesMuebles(GuardarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * numero de registro
                long numeroRegistro = 0;
                string codigoEvento = BienesMueblesEventos.GUARDAR_BIEN_MUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Obteniendo codigoNumeroRegistro...");
                    numeroRegistro = await _bienesMueblesRepository.ObtenerNumeroRegistroMax();
                    numeroRegistro += 1;
                    _logger.Informativo("Obtenido codigoNumeroRegistro...");

                    _logger.Informativo("Guardando BienesMuebles...");

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            obj.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                            obj.fechaUsuarioActualiza = DateTime.Now;
                            // * insercion
                            await _bienesMueblesRepository.GuardarBienesMuebles(
                                new GuardarBienesMueblesDto()
                                {
                                    codigoPersona = obj.codigoPersona,
                                    numeroRegistro = numeroRegistro,
                                    tipoBienMueble = obj.tipoBienMueble,
                                    codigoReferencia = obj.codigoReferencia,
                                    descripcion = obj.descripcion,
                                    avaluoComercial = obj.avaluoComercial,
                                    codigoUsuarioActualiza = obj.codigoUsuarioActualiza,
                                    fechaUsuarioActualiza = obj.fechaUsuarioActualiza,
                                    estado = '1'
                                }
                            );
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesMueblesEventos.BIEN_MUEBLE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Guardado BienesMuebles...");


                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_MUEBLES", obj);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarBienesMuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarBienesMuebles=> {ex}");
                        codigoEvento = BienesMueblesEventos.BIEN_MUEBLE_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarBienesMuebles(ActualizarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesMueblesEventos.ACTUALIZAR_BIEN_MUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Actualizando BienesMuebles");
                    int resultado = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            obj.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                            obj.fechaUsuarioActualiza = DateTime.Now;
                            resultado = await _bienesMueblesRepository.ActualizarBienesMuebles(
                                new ActualizarBienesMueblesDto()
                                {
                                    codigoPersona = obj.codigoPersona,
                                    numeroRegistro = obj.numeroRegistro,
                                    tipoBienMueble = obj.tipoBienMueble,
                                    codigoReferencia = obj.codigoReferencia,
                                    descripcion = obj.descripcion,
                                    avaluoComercial = obj.avaluoComercial,
                                    codigoUsuarioActualiza = obj.codigoUsuarioActualiza,
                                    fechaUsuarioActualiza =  obj.fechaUsuarioActualiza 
                                });
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesMueblesEventos.BIEN_MUEBLE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Actualizo BienesMuebles");
                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesMueblesEventos.BIEN_MUEBLE_NO_ACTUALIZADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_MUEBLES", obj);


                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarBienesMuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarBienesMuebles=> {ex}");
                        codigoEvento = BienesMueblesEventos.BIEN_MUEBLE_NO_ACTUALIZADO;
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

        public async Task<Respuesta> EliminarBienesMuebles(EliminarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesMueblesEventos.ELIMINAR_BIEN_MUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Eliminando BienesMuebles");
                    int resultado = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            // * historicos
                            obj.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                            obj.fechaUsuarioActualiza = DateTime.Now;

                            resultado = await _bienesMueblesRepository.EliminarBienesMuebles(
                                new EliminarBienesMueblesDto()
                                {
                                    codigoPersona = obj.codigoPersona,
                                    numeroRegistro = obj.numeroRegistro,
                                    codigoUsuarioActualiza = obj.codigoUsuarioActualiza,
                                    fechaUsuarioActualiza = obj.fechaUsuarioActualiza
                                });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Eliminado BienesMuebles");
                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesMueblesEventos.BIEN_MUEBLE_NO_ELIMINADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_MUEBLES", obj);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarBienesMuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarBienesMuebles=> {ex}");
                        codigoEvento = BienesMueblesEventos.BIEN_MUEBLE_NO_ELIMINADO;
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
        }
    }
}