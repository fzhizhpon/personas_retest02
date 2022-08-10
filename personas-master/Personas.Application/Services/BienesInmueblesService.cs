using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.BienesInmuebles;
using Personas.Core.Entities.BienesInmuebles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class BienesInmueblesService : IBienesInmueblesService
    {
        private readonly IBienesInmueblesRepository _bienesInmueblesRepository;

        private readonly ConfiguracionApp _config;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ILogsRepository<BienesInmuebles> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public BienesInmueblesService(
            IBienesInmueblesRepository bienesInmueblesRepository,
            ConfiguracionApp config,
            ILogsRepository<BienesInmuebles> logger,
            IMensajesRespuestaRepository textoInfoService,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _bienesInmueblesRepository = bienesInmueblesRepository;
            _config = config;
            _textoInfoService = textoInfoService;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ObtenerBienesInmuebles(ObtenerBienesInmueblesDto dto)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            IEnumerable<BienesInmuebles.Minimo> bienesInmuebles = null;
            string codigoEvento = BienesInmueblesEventos.OBTENER_BIENES_INMUEBLES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienesInmuebles");
                    (codigo, bienesInmuebles) = await _bienesInmueblesRepository.ObtenerBienesInmuebles(dto);
                    _logger.Informativo("Obtenido BienesInmuebles");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienesInmuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienesInmuebles=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIENES_INMUEBLES_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienesInmuebles
            };
        }

        public async Task<Respuesta> ObtenerBienInmueble(long codigoPersona, long numeroRegistro)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            BienesInmuebles bienInmueble = null;
            string codigoEvento = BienesInmueblesEventos.OBTENER_BIEN_INMUEBLE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienInmueble");
                    (codigo, bienInmueble) =
                        await _bienesInmueblesRepository.ObtenerBienInmueble(codigoPersona, numeroRegistro);
                    _logger.Informativo("Obtenido BienInmueble");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienInmueble => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienInmueble=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIEN_INMUEBLE_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienInmueble
            };
        }

        public async Task<Respuesta> GuardarBienesInmuebles(GuardarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * numero de registro
                long numeroRegistro = 0;
                // * codigos de eventos
                string codigoEvento = BienesInmueblesEventos.GUARDAR_BIEN_INMUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Obteniendo codigoNumeroRegistro...");
                    numeroRegistro = await _bienesInmueblesRepository.ObtenerNumeroRegistroMax();
                    numeroRegistro += 1;
                    _logger.Informativo("Obtenido codigoNumeroRegistro...");


                    _logger.Informativo("Guardando BienesInmuebles...");
                    // * objeto a guardar
                    GuardarBienesInmueblesDto bienesInmueblesDto = null;
                    await _dataBaseExceptions.CatchExceptionAsync(async () =>
                    {
                        bienesInmueblesDto = new GuardarBienesInmueblesDto()
                        {
                            codigoPersona = obj.codigoPersona,
                            numeroRegistro = numeroRegistro,
                            tipoBienInmueble = obj.tipoBienInmueble,
                            codigoPais = obj.codigoPais,
                            codigoProvincia = obj.codigoProvincia,
                            codigoCiudad = obj.codigoCiudad,
                            codigoParroquia = obj.codigoParroquia,
                            sector = obj.sector,
                            callePrincipal = obj.callePrincipal,
                            calleSecundaria = obj.calleSecundaria,
                            numero = obj.numero,
                            codigoPostal = obj.codigoPostal,
                            tipoSector = obj.tipoSector,
                            esMarginal = obj.esMarginal,
                            longitud = obj.longitud,
                            latitud = obj.latitud,
                            avaluoComercial = obj.avaluoComercial,
                            avaluoCatastral = obj.avaluoCatastral,
                            areaTerreno = obj.areaTerreno,
                            areaConstruccion = obj.areaConstruccion,
                            valorTerrenoMetrosCuadrados = obj.valorTerrenoMetrosCuadrados,
                            fechaConstruccion = obj.fechaConstruccion,
                            referencia = obj.referencia,
                            comunidad = obj.comunidad,
                            descripcion = obj.descripcion,
                            codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                            fechaUsuarioActualiza = DateTime.Now,
                            estado = '1'
                        };
                        // * insercion
                        await _bienesInmueblesRepository.GuardarBienesInmuebles(bienesInmueblesDto);
                    }, new DbExceptionEvents
                    {
                        ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_ERROR_FK),
                        CheckConstraint = () => throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_ERROR_CHECK_CONSTRAINT),
                        Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                    });
                    _logger.Informativo("Guardado BienesInmuebles...");
                    
                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INMUEBLES", bienesInmueblesDto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarBienesInmuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarBienesInmuebles=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIEN_INMUEBLE_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarBienesInmuebles(ActualizarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesInmueblesEventos.ACTUALIZAR_BIEN_INMUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Actualizando BienesInmuebles");
                    int resultado = 0;
                    // * objeto a actualizar
                    ActualizarBienesInmueblesDto bienesInmueblesDto = null;
                    await _dataBaseExceptions.CatchExceptionAsync(async () =>
                    {
                        bienesInmueblesDto = new ActualizarBienesInmueblesDto()
                        {
                            codigoPersona = obj.codigoPersona,
                            numeroRegistro = obj.numeroRegistro,
                            tipoBienInmueble = obj.tipoBienInmueble,
                            callePrincipal = obj.callePrincipal,
                            calleSecundaria = obj.calleSecundaria,
                            avaluoComercial = obj.avaluoComercial,
                            avaluoCatastral = obj.avaluoCatastral,
                            areaConstruccion = obj.areaConstruccion,
                            valorTerrenoMetrosCuadrados = obj.valorTerrenoMetrosCuadrados,
                            fechaConstruccion = obj.fechaConstruccion,
                            referencia = obj.referencia,
                            comunidad = obj.comunidad,
                            descripcion = obj.descripcion,
                            codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                            fechaUsuarioActualiza = DateTime.Now,
                        };
                        resultado = await _bienesInmueblesRepository.ActualizarBienesInmuebles(bienesInmueblesDto);
                    }, new DbExceptionEvents
                    {
                        ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_ERROR_FK),
                        CheckConstraint = () => throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_ERROR_CHECK_CONSTRAINT),
                        Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                    });
                    _logger.Informativo("Actualizado BienesInmuebles");

                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_NO_ACTUALIZADO);
                    }
                    
                    
                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INMUEBLES", bienesInmueblesDto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarBienesInmuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarBienesInmuebles=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIEN_INMUEBLE_NO_ACTUALIZADO;
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

        public async Task<Respuesta> EliminarBienesInmuebles(EliminarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // * codigos de eventos
                string codigoEvento = BienesInmueblesEventos.ELIMINAR_BIEN_INMUEBLE;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo("Eliminando BienesInmuebles");
                    int resultado = 0;

                    EliminarBienesInmueblesDto bienesInmueblesDto = new EliminarBienesInmueblesDto
                    {
                        codigoPersona = obj.codigoPersona,
                        numeroRegistro = obj.numeroRegistro,
                        codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                        fechaUsuarioActualiza = DateTime.Now,
                    };

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            resultado = await _bienesInmueblesRepository.EliminarBienesInmuebles(bienesInmueblesDto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });
                    _logger.Informativo("Eliminado BienesInmuebles");

                    if (resultado == 0)
                    {
                        throw new ExcepcionOperativa(BienesInmueblesEventos.BIEN_INMUEBLE_NO_ELIMINADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_BIENES_INMUEBLES", bienesInmueblesDto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarBienesInmuebles => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarBienesInmuebles=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIEN_INMUEBLE_NO_ELIMINADO;
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

        public async Task<Respuesta> ObtenerBienesInmueblesSinJoin(long codigoPersona)
        {
            // * variables para la respuesta de la query
            int codigo = 0;
            IEnumerable<BienesInmuebles.MinimoSinJoin> bienInmueble = null;
            string codigoEvento = BienesInmueblesEventos.OBTENER_BIENES_INMUEBLES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo("Obteniendo BienInmueble");
                    (codigo, bienInmueble) =
                        await _bienesInmueblesRepository.ObtenerBienesInmueblesSinJoin(codigoPersona);
                    _logger.Informativo("Obtenido BienInmueble");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerBienInmueble => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerBienInmueble=> {ex}");
                        codigoEvento = BienesInmueblesEventos.BIENES_INMUEBLES_NO_OBTENIDOS;
                    }
                }
            }

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(_config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = textoInfo,
                resultado = bienInmueble
            };
        }
    }
}