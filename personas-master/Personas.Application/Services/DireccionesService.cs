using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.EnumsEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Direcciones;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.TelefonosFijos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;
using VimaCoop.Auditoria.Interfaces;

namespace Personas.Application.Services
{
    public class DireccionesService : IDireccionesService
    {
        private readonly IDireccionesRepository _direccionesRepository;
        private readonly IMensajesRespuestaRepository _textoInfoService;
        private readonly ConfiguracionApp _config;
        private readonly ILogsRepository<DireccionesService> _logger;
        private readonly IPersonaRepository _personaRepository;
        private readonly ITelefonosFijosRepository _telefonosFijosRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public DireccionesService(IDireccionesRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<DireccionesService> logger,
            ConfiguracionApp config,
            IPersonaRepository personaRepository,
            ITelefonosFijosRepository telefonosFijosRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _direccionesRepository = repository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _personaRepository = personaRepository;
            _telefonosFijosRepository = telefonosFijosRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarDireccion(GuardarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                int numeroRegistroDireccion = 0;
                int numeroRegistroTelefono = 0;
                string codigoEvento = DireccionesEventos.GUARDAR_DIRECCION; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    if (dto.principal == '1')
                    {
                        await _direccionesRepository.DesmarcarDireccionPrincipal(dto.codigoPersona);
                    }
                    else
                    {
                        if ((await _direccionesRepository.NroDireccionesPrincipales(dto.codigoPersona)) == 0)
                        {
                            dto.principal = '1';
                        }
                    }

                    numeroRegistroDireccion =
                        await _direccionesRepository.ObtenerCodigoNuevaDireccion(dto.codigoPersona);
                    numeroRegistroTelefono = await _telefonosFijosRepository.GenerarNumeroRegistro(dto.codigoPersona);

                    dto.numeroRegistro = numeroRegistroDireccion;
                    dto.codigoUsuarioRegistra = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioRegistra = DateTime.Now;


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { await _direccionesRepository.GuardarDireccion(dto); }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(DireccionesEventos.DIRECCION_ERROR_FK),
                            CheckConstraint = () => throw new ExcepcionOperativa(DireccionesEventos.DIRECCION_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => {
                            await _telefonosFijosRepository.GuardarTelefonoFijo(new GuardarTelefonoFijoDto()
                            {
                                codigoPersona = dto.codigoPersona,
                                codigoDireccion = numeroRegistroDireccion,
                                numeroRegistro = numeroRegistroTelefono,
                                numero = dto.telefonoFijo.numero,
                                codigoOperadora = dto.telefonoFijo.codigoOperadora,
                                observaciones = dto.telefonoFijo.observaciones,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        },
                        new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    await _personaRepository.ColocarFechaUltimaActualizacion(new UltActPersonaRequest
                    {
                        codigoPersona = dto.codigoPersona,
                        fechaUsuarioActualiza = DateTime.Now,
                        codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                    });

                    _logger.Informativo($"AgregarDireccion => {codigoEvento}");

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_DIRECCIONES", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"AgregarDireccion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"AgregarDireccion => {ex}");
                        codigoEvento = DireccionesEventos.DIRECCION_NO_GUARDADO;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = numeroRegistroDireccion
                };
            }
        }

        public async Task<Respuesta> ActualizarDireccion(ActualizarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string codigoEvento = DireccionesEventos.ACTUALIZAR_DIRECCION; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    if (dto.principal == '1')
                    {
                        await _direccionesRepository.DesmarcarDireccionPrincipal(dto.codigoPersona);
                    }
                    else
                    {
                        if ((await _direccionesRepository.NroDireccionesPrincipales(dto.codigoPersona)) == 0)
                        {
                            dto.principal = '1';
                        }
                    }

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    int afectados = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { afectados = await _direccionesRepository.ActualizarDireccion(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(DireccionesEventos.DIRECCION_ERROR_FK),
                            CheckConstraint = () => throw new ExcepcionOperativa(DireccionesEventos.DIRECCION_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (afectados == 0)
                    {
                        codigoEvento = DireccionesEventos.DIRECCION_NO_ACTUALIZADO; // Se guardo el correo
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => {
                            await _telefonosFijosRepository.ActualizarTelefonoFijo(new ActualizarTelefonoFijoDto()
                            {
                                codigoPersona = dto.codigoPersona,
                                codigoDireccion = dto.numeroRegistro,
                                numeroRegistro = dto.telefonoFijo.numeroRegistro,
                                numero = dto.telefonoFijo.numero,
                                codigoOperadora = dto.telefonoFijo.codigoOperadora,
                                observaciones = dto.telefonoFijo.observaciones,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

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

                    _logger.Informativo($"ActualizarDireccion => {codigoEvento}");
                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_DIRECCIONES", dto);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarDireccion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarDireccion => {ex}");
                        codigoEvento = DireccionesEventos.DIRECCION_NO_ACTUALIZADO;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null
                };
            }
        }

        public async Task<Respuesta> EliminarDireccion(EliminarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string codigoEvento = DireccionesEventos.ELIMINAR_DIRECCION; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => {
                            await _telefonosFijosRepository.EliminarTelefonoFijo(new EliminarTelefonoFijoDto()
                            {
                                codigoPersona = dto.codigoPersona,
                                numeroRegistro = dto.numeroRegistro,
                                fechaUsuarioActualiza = DateTime.Now,
                                codigoUsuarioActualiza = _config.codigoUsuarioRegistra
                            });
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(DireccionesEventos.DIRECCION_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { await _direccionesRepository.EliminarDireccion(dto); }, new DbExceptionEvents
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });


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


                    _logger.Informativo($"EliminarDireccion => {codigoEvento}");
                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_DIRECCIONES", dto);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarDireccion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarDireccion => {ex}");
                        codigoEvento = DireccionesEventos.DIRECCION_NO_ELIMINADO;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null
                };
            }
        }

        public async Task<Respuesta> ObtenerDireccion(ObtenerDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                DireccionOutDto direccion = new DireccionOutDto();
                string codigoEvento = DireccionesEventos.OBTENER_DIRECCION; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    direccion = await _direccionesRepository.ObtenerDireccion(dto);

                    direccion.direccion.numeroRegistro = dto.numeroRegistro;

                    _logger.Informativo($"ObtenerDirecciones => {codigoEvento}");

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerDirecciones => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerDirecciones => {ex}");
                        codigoEvento = DireccionesEventos.DIRECCION_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = direccion
                };
            }
        }

        public async Task<Respuesta> ObtenerDirecciones(ObtenerDireccionesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                List<DireccionMinResponse> direcciones = new List<DireccionMinResponse>();
                string codigoEvento = DireccionesEventos.OBTENER_DIRECCIONES; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    direcciones = await _direccionesRepository.ObtenerDirecciones(dto);

                    _logger.Informativo($"ObtenerDirecciones => {codigoEvento}");

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerDirecciones => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerDirecciones => {ex}");
                        codigoEvento = DireccionesEventos.DIRECCIONES_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = direcciones
                };
            }
        }
    }
}