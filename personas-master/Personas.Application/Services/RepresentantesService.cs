using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Personas;
using Personas.Core.Dtos.Representantes;
using Personas.Core.Entities.Representantes;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class RepresentantesService : IRepresentantesService
    {
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<RepresentantesService> _logger;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly IRepresentantesRepository _representantesRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public RepresentantesService(
            ConfiguracionApp config,
            ILogsRepository<RepresentantesService> logger,
            IMensajesRespuestaRepository textoInfoService,
            IRepresentantesRepository representantesRepository,
            IPersonaRepository personaRepository,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _logger = logger;
            _config = config;
            _textoInfoService = textoInfoService;
            _representantesRepository = representantesRepository;
            _personaRepository = personaRepository;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarRepresentante(GuardarRepresentanteDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            IList<Representante.RepresentanteSimple> trabajos = new List<Representante.RepresentanteSimple>();
            string codigoEvento = RepresentantesEventos.GUARDAR_REPRESENTANTE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    if (dto.codigoPersona == dto.codigoRepresentante)
                    {
                        codigoEvento = RepresentantesEventos.GUARDAR_REPRESENTANTE_ERROR_AUTO_REPRESENTACION;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                    }

                    int result = 0;

                    _logger.Informativo($"Existe representante...");

                    var existeReferencia = await _representantesRepository.obtenerRepresentanteCodigoPersonaCodigoRepre(
                        dto.codigoPersona,
                        dto.codigoRepresentante
                    );

                    if (existeReferencia is not null)
                    {
                        throw new ExcepcionOperativa(RepresentantesEventos.REPRESENTANTE_EXISTE);
                    }

                    _logger.Informativo($"Existio representante...");


                    // _logger.Informativo($"Verificando si es una persona natural o juridica...");
                    //
                    // var persona =  _personaRepository.ObtenerPersonas(new PersonaRequest()
                    // {
                    //     codigoPersona = dto.codigoPersona
                    // });
                    //
                    // // * obtenemos el tipo de persona a la cual se quiere ingresar el representante
                    // var tipoPersona = persona[0].codigoTipoPersona;
                    //
                    //
                    // if (tipoPersona == 2)
                    // {
                    //     var representanteExisteJudicial = _representantesRepository.ObtenerRepresentantesFiltros(new RepresentanteRequest()
                    //     {
                    //         codigoPersona = dto.codigoPersona,
                    //         estado = '1',
                    //         codigoTipoRepresentante = 1
                    //     });
                    //     
                    //     // * verificamos si ya existe un representante
                    //     Console.WriteLine(representanteExisteJudicial);
                    // }
                    //
                    // _logger.Informativo($"Verificado si es una persona natural o juridica...");


                    _logger.Informativo($"Verificando representante...");

                    var resultadoVerificacion =
                        await _representantesRepository.esUnRepresetanteEliminado(
                            dto.codigoPersona,
                            dto.codigoRepresentante
                        );

                    if (resultadoVerificacion is not null)
                    {
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _representantesRepository.reactivarRepresentante(
                                    new
                                    {
                                        dto.codigoTipoRepresentante,
                                        dto.principal,
                                        dto.codigoUsuarioActualiza,
                                        dto.fechaUsuarioActualiza,
                                        dto.codigoPersona,
                                        dto.codigoRepresentante
                                    });
                            },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(RepresentantesEventos.REPRESENTANTE_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }
                    else
                    {
                        // * no existe previamente  el representante ingresado se procede a guardar
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () => { result = await _representantesRepository.GuardarRepresentante(dto); },
                            new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(RepresentantesEventos.REPRESENTANTE_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }

                    _logger.Informativo($"Verificado representante...");

                    if (dto.principal == '1')
                    {
                        trabajos = await _representantesRepository.ObtenerRepresentantesPrincipales(
                            new ObtenerRepresentantesDto()
                            {
                                codigoPersona = dto.codigoPersona
                            });
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                await _representantesRepository.ActualizarRepresentantesPrincipales(trabajos);
                            }, new DbExceptionEvents
                            {
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }

                    _logger.Informativo($"Guardando representante...");

                    if (result == 0)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_GUARDADO;
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

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REPRESENTANTES", dto);

                    scope.Complete();

                    _logger.Informativo($"Representante guardado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_GUARDADO;
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

        public async Task<Respuesta> ActualizarRepresentante(ActualizarRepresentanteDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;
            dto.guid = _config.guid;

            IList<Representante.RepresentanteSimple> trabajos = new List<Representante.RepresentanteSimple>();
            string codigoEvento = RepresentantesEventos.ACTUALIZAR_REPRESENTANTE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    _logger.Informativo($"Actualizando representante...");

                    int result = 0;

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _representantesRepository.ActualizarRepresentante(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(RepresentantesEventos.REPRESENTANTE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_ACTUALIZADO;
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

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REPRESENTANTES", dto);
                    scope.Complete();

                    _logger.Informativo($"Representante actualizado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_ACTUALIZADO;
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

        public async Task<Respuesta> EliminarRepresentante(EliminarRepresentanteDto dto)
        {
            dto.fechaUsuarioActualiza = DateTime.Now;
            dto.codigoUsuarioActualiza = _config.codigoUsuarioRegistra;

            string codigoEvento = RepresentantesEventos.ELIMINAR_REPRESENTANTE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _logger.Informativo($"Eliminando representante...");

                    int result = 0;
                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { result = await _representantesRepository.EliminarRepresentante(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(RepresentantesEventos.REPRESENTANTE_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_ELIMINADO;
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

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_REPRESENTANTES", dto);
                    scope.Complete();

                    _logger.Informativo($"Representante eliminado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"EliminarRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"EliminarRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_ELIMINADO;
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

        public async Task<Respuesta> ObtenerRepresentante(int codigoPersona, int codigoRepresentante)
        {
            ObtenerRepresentanteDto dto = new ObtenerRepresentanteDto()
            {
                codigoPersona = codigoPersona,
                codigoRepresentante = codigoRepresentante
            };

            Representante.RepresentanteJoin representante = null;
            string codigoEvento = RepresentantesEventos.OBTENER_REPRESENTANTE;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _logger.Informativo($"Consultando representante...");

                    representante = await _representantesRepository.ObtenerRepresentante(dto);
                    scope.Complete();

                    if (representante == null)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Representante consultado");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTE_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = representante
            };
        }

        public async Task<Respuesta> ObtenerRepresentantes(int codigoPersona)
        {
            ObtenerRepresentantesDto dto = new ObtenerRepresentantesDto()
            {
                codigoPersona = codigoPersona
            };

            IList<Representante.RepresentanteJoin> representantes = new List<Representante.RepresentanteJoin>();
            string codigoEvento = RepresentantesEventos.OBTENER_REPRESENTANTES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _logger.Informativo($"Consultando representantes...");

                    representantes = await _representantesRepository.ObtenerRepresentantes(dto);
                    scope.Complete();

                    if (representantes == null)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTES_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Representantes consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;


                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTES_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = representantes
            };
        }

        public async Task<Respuesta> ObtenerRepresentantesFiltros(RepresentanteRequest representanteRequest)
        {
            IList<Representante.RepresentanteJoin> representantes = new List<Representante.RepresentanteJoin>();
            string codigoEvento = RepresentantesEventos.OBTENER_REPRESENTANTES;
            int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _logger.Informativo($"Consultando representantes...");

                    representantes =  _representantesRepository.ObtenerRepresentantesFiltros(representanteRequest);
                
                    if (representantes == null)
                    {
                        codigoEvento = RepresentantesEventos.REPRESENTANTES_NO_OBTENIDOS;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }

                    _logger.Informativo($"Representantes consultados");
                }
                catch (Exception exc)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;


                    if (exc is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"GuardarRepresentante => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = excOperativa.codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"GuardarRepresentante => {exc}");
                        codigoEvento = RepresentantesEventos.REPRESENTANTES_NO_OBTENIDOS;
                    }
                }
            }

            string mensaje = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                mensaje = mensaje,
                resultado = representantes
            };
        }
    }
}