using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.CorreosElectronicos;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.CorreosElectronicos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class CorreosElectronicosService : ICorreosElectronicosService
    {
        protected readonly ICorreosElectronicosRepository _correosRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<DireccionesService> _logger;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public CorreosElectronicosService(ICorreosElectronicosRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<DireccionesService> logger,
            ConfiguracionApp config,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _correosRepository = repository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ObtenerCorreos(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string codigoEvento = CorreosElectronicosEventos.OBTENER_CORREOS; // Se obtuvieron los correos
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
                List<CorreoElectronicoDto> correos = new List<CorreoElectronicoDto>();

                try
                {
                    correos = _correosRepository.ObtenerCorreos(codigoPersona);
                    scope.Complete();
                    _logger.Informativo($"ObtenerCorreos => {codigoEvento}");
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerCorreos => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerCorreos => {ex}");
                        codigoEvento = CorreosElectronicosEventos.CORREOS_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = correos
                };
            }
        }

        public async Task<Respuesta> AgregarCorreo(AgregarCorreoElectronicoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                int codigoCorreo = -1;
                string codigoEvento = CorreosElectronicosEventos.GUARDAR_CORREO;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    CorreoElectronico correoBuscado = new CorreoElectronico();
                    
                    _dataBaseExceptions.CatchException(
                        () =>
                        {
                            correoBuscado = _correosRepository.ObtenerCorreo(dto.codigoPersona, dto.correoElectronico);
                        }, new DbExceptionEvents()
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });
                    
                    if (correoBuscado is not null)
                    {
                        throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_EXISTE);
                    }
                    
                    if (dto.esPrincipal == '1')
                    {
                        _correosRepository.DesmarcarCorreoPrincipal(dto.codigoPersona);
                    }
                    else
                    {
                        if (_correosRepository.NroCorreosPrincipales(dto.codigoPersona) == 0)
                        {
                            dto.esPrincipal = '1';
                        }
                    }


                    codigoCorreo = _correosRepository.ObtenerCodigoNuevoCorreo(dto.codigoPersona);


                    _dataBaseExceptions.CatchException(
                        () => { _correosRepository.AgregarCorreo(codigoCorreo, dto); }, new DbExceptionEvents()
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_ELECTRONICO_ERROR_FK),
                            CheckConstraint = () => throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_ELECTRONICO_ERROR_CHECK_CONSTRAINT),
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


                    _logger.Informativo($"AgregarCorreo => {codigoEvento}");

                    dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                    dto.fechaUsuarioActualiza = DateTime.Now;
                    
                    await _auditoria.AuditarAsync("PERS_CORREOS_ELECTRONICOS", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"AgregarCorreo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"AgregarCorreo => {ex}");
                        codigoEvento = CorreosElectronicosEventos.CORREO_NO_GUARDADO;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = codigoCorreo
                };
            }
        }

        public async Task<Respuesta> ActualizarCorreo(ActualizarCorreoElectronicoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string codigoEvento = CorreosElectronicosEventos.ACTUALIZAR_CORREO; // Se actualizo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    CorreoElectronico correo = new CorreoElectronico();

                    // Validamos que el correo a actualizar no exista

                    #region

                    _dataBaseExceptions.CatchException(
                        () => { correo = _correosRepository.ObtenerCorreo(dto.codigoPersona, dto.correoElectronico); },
                        new DbExceptionEvents()
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (correo != null)
                    {
                        if (correo.codigoCorreoElectronico != dto.codigoCorreoElectronico)
                        {
                            throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_EXISTE);
                        }
                    }

                    #endregion

                    _dataBaseExceptions.CatchException(
                        () =>
                        {
                            correo = _correosRepository.ObtenerCorreo(dto.codigoPersona, dto.codigoCorreoElectronico);
                        }, new DbExceptionEvents()
                        {
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (correo is null)
                    {
                        throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_NO_OBTENIDO);
                    }

                    correo.correoElectronico = dto.correoElectronico;
                    correo.esPrincipal = dto.esPrincipal;
                    correo.observaciones = dto.observaciones;
                    correo.fechaActualiza = DateTime.Now;
                    correo.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

                    if (correo.esPrincipal == '1')
                    {
                        _correosRepository.DesmarcarCorreoPrincipal(dto.codigoPersona);
                    }
                    else
                    {
                        if (_correosRepository.NroCorreosPrincipales(dto.codigoPersona) == 0)
                        {
                            correo.esPrincipal = '1';
                        }
                    }

                    _dataBaseExceptions.CatchException(
                        () => { _correosRepository.ActualizarCorreo(correo); }, new DbExceptionEvents()
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_ELECTRONICO_ERROR_FK),
                            CheckConstraint = () => throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_ELECTRONICO_ERROR_CHECK_CONSTRAINT),
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

                    await _auditoria.AuditarAsync("PERS_CORREOS_ELECTRONICOS", correo);

                    _logger.Informativo($"ActualizarCorreo => {codigoEvento}");
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarCorreo => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarCorreo => {ex}");
                        codigoEvento = CorreosElectronicosEventos.CORREO_NO_ACTUALIZADO;
                    }
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                scope.Dispose();

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null
                };
            }
        }

        public async Task<Respuesta> EliminarCorreo(EliminarCorreoRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string codigoEvento = CorreosElectronicosEventos.ELIMINAR_CORREO; // Se guardo el correo
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _dataBaseExceptions.CatchException(
                        () =>
                        {
                            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
                            dto.fechaUsuarioActualiza = DateTime.Now;
                            _correosRepository.EliminarCorreo(dto);
                        }, new DbExceptionEvents()
                        {
                            ForeignKeyViolation = () => throw new ExcepcionOperativa(CorreosElectronicosEventos.CORREO_ELECTRONICO_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });
                    _logger.Informativo($"EliminarCorreo => {codigoEvento}");
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

                    await _auditoria.AuditarAsync("PERS_CORREOS_ELECTRONICOS", dto);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerCorreos => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerCorreos => {ex}");
                        codigoEvento = CorreosElectronicosEventos.CORREO_NO_ELIMINADO;
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
    }
}