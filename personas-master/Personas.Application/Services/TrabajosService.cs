using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.Trabajos;
using Personas.Core.Entities.Trabajos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class TrabajosService : ITrabajosService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<TrabajosService> _logger;
        protected readonly ITrabajosRepository _trabajosRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public TrabajosService(
            ConfiguracionApp config,
            ILogsRepository<TrabajosService> logger,
            ITrabajosRepository serviceTrabajosRepository,
            IMensajesRespuestaRepository textoInfoService,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _trabajosRepository = serviceTrabajosRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarTrabajo(GuardarTrabajoDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = TrabajosEventos.GUARDAR_TRABAJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Existe trabajo...");

                    var existeReferencia = await _trabajosRepository.obtenerTrabajoCodigoPersonaRazonSocial(
                        dto.codigoPersona,
                        dto.razonSocial
                    );

                    if (existeReferencia is not null)
                    {
                        throw new ExcepcionOperativa(TrabajosEventos.TRABAJO_EXISTE);
                    }

                    _logger.Informativo($"Existio trabajo...");

                    _logger.Informativo($"Verificando trabajo...");

                    var resultadoVerificacion =
                        await _trabajosRepository.esUnTrabajoEliminado(dto.codigoPersona, dto.razonSocial);

                    _logger.Informativo($"Verificado trabajo...");

                    if (resultadoVerificacion is not null)
                    {
                        // * si existe previamente el trabajo ingresado se lo va a volver a reactivar y modificar los campos
                        _logger.Informativo($"Guardando trabajo...");
                        int result = 0;

                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _trabajosRepository.reactivarTrabajo(
                                    new
                                    {
                                        dto.codigoCategoria,
                                        dto.fechaIngreso,
                                        dto.razonSocial,
                                        dto.direccion,
                                        dto.cargo,
                                        dto.codigoPais,
                                        dto.codigoProvincia,
                                        dto.codigoCiudad,
                                        dto.codigoParroquia,
                                        dto.ingresosMensuales,
                                        dto.codigoUsuarioActualiza,
                                        dto.fechaUsuarioActualiza,
                                        dto.principal,
                                        dto.codigoPersona
                                    }
                                );
                            },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(TrabajosEventos.TRABAJO_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });

                        if (result == 0)
                        {
                            codigoEvento = TrabajosEventos.TRABAJO_NO_GUARDADO;
                            codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                            throw new ExcepcionOperativa(codigoEvento);
                        }

                        _logger.Informativo($"Trabajo guardado");
                    }
                    else
                    {
                        // * no existe previamente  el trabajo ingresadose procede a guardar

                        _logger.Informativo($"Guardando trabajo...");
                        int codigoTrabajo = await _trabajosRepository.ObtenerCodigoTrabajo(dto.codigoPersona);
                        dto.codigoTrabajo = codigoTrabajo + 1;

                        int result = 0;
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () => { result = await _trabajosRepository.GuardarTrabajo(codigoTrabajo + 1, dto); },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(TrabajosEventos.TRABAJO_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });

                        if (result == 0)
                        {
                            codigoEvento = TrabajosEventos.TRABAJO_NO_GUARDADO;
                            codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                            throw new ExcepcionOperativa(codigoEvento);
                        }

                        _logger.Informativo($"Trabajo guardado");
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
                    await _auditoria.AuditarAsync("PERS_TRABAJOS", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarTrabajo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarTrabajo => {exc}");
                        codigoEvento = TrabajosEventos.TRABAJO_NO_GUARDADO;
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

        public async Task<Respuesta> ObtenerTrabajo(ObtenerTrabajoDto dto)
        {
            Trabajo trabajo = null;
            string codigoEvento = TrabajosEventos.OBTENER_TRABAJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando trabajo...");

                    trabajo = await _trabajosRepository.ObtenerTrabajo(dto);

                    if (trabajo == null)
                    {
                        codigoEvento = TrabajosEventos.TRABAJO_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    scope.Complete();

                    _logger.Informativo($"Trabajo consultado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerTrabajo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerTrabajo => {exc}");
                        codigoEvento = TrabajosEventos.TRABAJO_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = trabajo
            };
        }

        public async Task<Respuesta> ObtenerTrabajos(ObtenerTrabajosDto dto)
        {
            IList<Trabajo.TrabajoMinimo> trabajos = new List<Trabajo.TrabajoMinimo>();
            string codigoEvento = TrabajosEventos.OBTENER_TRABAJOS;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Consultando trabajos...");

                    trabajos = await _trabajosRepository.ObtenerTrabajos(dto);
                    scope.Complete();

                    if (trabajos == null)
                    {
                        codigoEvento = TrabajosEventos.TRABAJOS_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Trabajos consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerTrabajos => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerTrabajos => {exc}");
                        codigoEvento = TrabajosEventos.TRABAJOS_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = trabajos
            };
        }

        public async Task<Respuesta> EliminarTrabajo(EliminarTrabajoDto dto)
        {
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.guid = _config.guid;

            string codigoEvento = TrabajosEventos.ELIMINAR_TRABAJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    // Consultar trabajo para guardar en repoitorio
                    Trabajo trabajo = await _trabajosRepository.ObtenerTrabajo(new ObtenerTrabajoDto()
                    {
                        codigoPersona = dto.codigoPersona,
                        codigoTrabajo = dto.codigoTrabajo
                    });

                    _logger.Informativo($"Eliminar trabajo...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _trabajosRepository.EliminarTrabajo(dto); }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TrabajosEventos.TRABAJO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = TrabajosEventos.TRABAJO_NO_ELIMINADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else
                    {
                        trabajo.codigoUsuarioRegistro = _config.codigoUsuarioRegistra;
                        trabajo.fechaUsuarioRegistro = DateTime.Now;
                        trabajo.guid = _config.guid;


                        if (result > 1)
                        {
                            _logger.Warning($"Trabajo eliminado con {result} registros afectados!");
                        }
                        else
                        {
                            _logger.Informativo($"Trabajo eliminado");
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
                    await _auditoria.AuditarAsync("PERS_TRABAJOS", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarTrabajo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarTrabajo => {exc}");
                        codigoEvento = TrabajosEventos.TRABAJO_NO_ELIMINADO;
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

        public async Task<Respuesta> ActualizarTrabajo(ActualizarTrabajoDto dto)
        {
            string codigoEvento = TrabajosEventos.ACTUALIZAR_TRABAJO;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    dto.fechaUsuarioActualiza = DateTime.Now;
                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

                    // Consultar trabajo para guardar en repoitorio
                    Trabajo trabajo = await _trabajosRepository.ObtenerTrabajo(new ObtenerTrabajoDto()
                    {
                        codigoPersona = dto.codigoPersona,
                        codigoTrabajo = dto.codigoTrabajo
                    });

                    if (trabajo == null)
                    {
                        throw new ExcepcionOperativa(TrabajosEventos.OBTENER_TRABAJO_ERROR);
                    }

                    _logger.Informativo($"Actualizando trabajo...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _trabajosRepository.ActualizarTrabajo(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TrabajosEventos.TRABAJO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = TrabajosEventos.TRABAJO_NO_ACTUALIZADO; // No se guardo el trabajo
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }
                    else
                    {
                        trabajo.codigoUsuarioRegistro = _config.codigoUsuarioRegistra;
                        trabajo.fechaUsuarioRegistro = DateTime.Now;
                        trabajo.guid = _config.guid;

                        //await _trabajosRepository.GuardarTrabajoHistorico(trabajo);

                        if (result > 1)
                        {
                            _logger.Warning($"Trabajo actualizado con {result} registros afectados!");
                        }
                        else
                        {
                            _logger.Informativo($"Trabajo actualizado");
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
                    await _auditoria.AuditarAsync("PERS_TRABAJOS", dto);

                    scope.Complete();
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarTrabajo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarTrabajo => {exc}");
                        codigoEvento = TrabajosEventos.TRABAJO_NO_ACTUALIZADO;
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