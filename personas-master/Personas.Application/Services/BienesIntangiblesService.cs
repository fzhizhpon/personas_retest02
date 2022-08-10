using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.BienesIntangibles;
using Personas.Core.Entities.BienesIntangibles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class BienesIntangiblesService : IBienesIntangiblesService

    {
        private readonly IBienesIntangiblesRepository _bienesIntangiblesRepository;
        private readonly ConfiguracionApp _config; // configuracion
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<BienesIntangibles> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public BienesIntangiblesService(
            IBienesIntangiblesRepository bienesIntangiblesRepository,
            ConfiguracionApp config,
            ILogsRepository<BienesIntangibles> logger,
            IMensajesRespuestaRepository textoInfoService,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _bienesIntangiblesRepository = bienesIntangiblesRepository;
            _config = config;
            _textoInfoService = textoInfoService;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }


        public async Task<Respuesta> ObtenerBienesIntangibles(long codigoPersona)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            IEnumerable<BienesIntangibles> bienesIntangibles = null;
            string codigoEvento = BienesIntangiblesEventos.OBTENER_BIENES_INMUEBLES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienesIntangibles");
                    (codigo, bienesIntangibles) =
                        await _bienesIntangiblesRepository.ObtenerBienesIntangibles(codigoPersona);
                    _logger.Informativo("Obtenido BienesIntangibles");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienesIntangibles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienesIntangibles=> {ex}");
                        codigoEvento = BienesIntangiblesEventos.BIENES_INTANGIBLES_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienesIntangibles
            };
        }

        public async Task<Respuesta> ObtenerBienIntangible(long codigoPersona, long numeroRegistro)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            BienesIntangibles bienIntangible = null;
            string codigoEvento = BienesIntangiblesEventos.OBTENER_BIEN_INTANGIBLE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienIntangible");
                    (codigo, bienIntangible) =
                        await _bienesIntangiblesRepository.ObtenerBienIntangible(codigoPersona, numeroRegistro);
                    _logger.Informativo("Obtenido BienIntangible");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienIntangible => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienIntangible=> {ex}");
                        codigoEvento = BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienIntangible
            };
        }

        public async Task<Respuesta> GuardarBienesIntangibles(GuardarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * numero de registro
                long numeroRegistro = 0;
                // * codigos de eventos
                string codigoEvento = BienesIntangiblesEventos.GUARDAR_BIEN_INTANGIBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Obteniendo codigoNumeroRegistro...");
                    // recuperamos el ultimo id ingresado
                    numeroRegistro = await _bienesIntangiblesRepository.ObtenerNumeroRegistroMax();
                    numeroRegistro += 1;
                    _logger.Informativo("Obtenido codigoNumeroRegistro...");

                    _logger.Informativo("Guardando BienesIntangibles...");

                    GuardarBienesIntangiblesDto bienesIntangiblesDto = null;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            bienesIntangiblesDto = new GuardarBienesIntangiblesDto()
                            {
                                codigoPersona = obj.codigoPersona,
                                numeroRegistro = numeroRegistro,
                                tipoBienIntangible = obj.tipoBienIntangible,
                                codigoReferencia = obj.codigoReferencia,
                                descripcion = obj.descripcion,
                                avaluoComercial = obj.avaluoComercial,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now,
                                estado = '1'
                            };
                            // * insercion
                            await _bienesIntangiblesRepository.GuardarBienesIntangibles(bienesIntangiblesDto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesIntangiblesEventos.BIEN_INTANGIBLE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INTANGIBLES", bienesIntangiblesDto);

                    _logger.Informativo("Guardado BienesIntangibles...");

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarBienesIntangibles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarBienesIntangibles=> {ex}");
                        codigoEvento = BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarBienesIntangibles(ActualizarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesIntangiblesEventos.ACTUALIZAR_BIEN_INTANGIBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Actualizando BienesIntangibles");

                    int resultado = 0;

                    ActualizarBienesIntangiblesDto bienesIntangiblesDto = null;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            bienesIntangiblesDto = new ActualizarBienesIntangiblesDto()
                            {
                                codigoPersona = obj.codigoPersona,
                                numeroRegistro = obj.numeroRegistro,
                                descripcion = obj.descripcion,
                                avaluoComercial = obj.avaluoComercial,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now
                            };
                            resultado =
                                await _bienesIntangiblesRepository.ActualizarBienesIntangibles(bienesIntangiblesDto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesIntangiblesEventos.BIEN_INTANGIBLE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Actualizado BienesIntangibles");


                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_ACTUALIZADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INTANGIBLES", bienesIntangiblesDto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarBienesIntangibles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        ;
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarBienesIntangibles=> {ex}");
                        codigoEvento = BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_ACTUALIZADO;
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

        public async Task<Respuesta> EliminarBienesIntangibles(EliminarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesIntangiblesEventos.ELIMINAR_BIEN_INTANGIBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Eliminando BienesIntangibles");

                    int resultado = 0;
                    EliminarBienesIntangiblesDto bienesIntangiblesDto = null;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            bienesIntangiblesDto = new EliminarBienesIntangiblesDto()
                            {
                                codigoPersona = obj.codigoPersona,
                                numeroRegistro = obj.numeroRegistro,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                fechaUsuarioActualiza = DateTime.Now
                            };
                            resultado =
                                await _bienesIntangiblesRepository.EliminarBienesIntangibles(bienesIntangiblesDto);
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    _logger.Informativo("Eliminado BienesIntangibles");

                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_ELIMINADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INTANGIBLES", bienesIntangiblesDto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarBienesIntangibles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarBienesIntangibles=> {ex}");
                        codigoEvento = BienesIntangiblesEventos.BIEN_INTANGIBLE_NO_ELIMINADO;
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