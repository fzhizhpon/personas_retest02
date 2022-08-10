using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.RelacionInstitucion;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class RelacionInstitucionService : IRelacionInstitucionService
    {
        protected readonly IRelacionInstitucionRepository _relacionInstitucionRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<RelacionInstitucionService> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public RelacionInstitucionService(IRelacionInstitucionRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<RelacionInstitucionService> logger,
            ConfiguracionApp config,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _relacionInstitucionRepository = repository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> ActualizarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int result = 0;
                string codigoEvento = RelacionInstitucionEventos.ACTUALIZAR_RELACION_INSTITUCIONAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    _logger.Informativo($"Actualizando relacion institucion");

                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            result = await _relacionInstitucionRepository.ActualizarPersonaRelacionInstitucion(dto);
                        }, new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(RelacionInstitucionEventos.RELACION_INSTITUCIONAL_ERROR_FK),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    if (result == 0)
                    {
                        throw new ExcepcionOperativa(RelacionInstitucionEventos.RELACION_INSTITUCIONAL_NO_ACTUALIZADO);
                    }

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_PERSONAS_RELACION_INSTITUCION", dto);

                    scope.Complete();

                    _logger.Informativo($"Persona relacion institucion");
                }
                catch (Exception ex)
                {
                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ActualizarRelacionInstitucion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ActualizarRelacionInstitucion => {ex}");
                        codigoEvento = RelacionInstitucionEventos.RELACION_INSTITUCIONAL_NO_ACTUALIZADO;
                    }
                }

                _logger.Informativo($"ActualizarRelacionInstitucion => {codigoEvento}");

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

        public async Task<Respuesta> GuardarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))

            {
                string codigoEvento = RelacionInstitucionEventos.GUARDAR_RELACION_INSTITUCIONAL;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


                try
                {
                    dto.usuarioAsigna = _config.codigoUsuarioRegistra;
                    dto.codigoAgenciaRegistra = _config.codigoAgencia;
                    PersonaRelacionInstitucion personaRelacionInstitucion =
                        await _relacionInstitucionRepository.ObtenerRelacionInstitucionMin(dto);

                    int result = 0;
                    if (personaRelacionInstitucion != null)
                    {
                        dto.estado = "1";
                        dto.fechaAsignacion = DateTime.Now;

                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _relacionInstitucionRepository.ActualizarPersonaRelacionInstitucion(dto);
                            }, new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(RelacionInstitucionEventos.RELACION_INSTITUCIONAL_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }
                    else
                    {
                        await _dataBaseExceptions.CatchExceptionAsync(
                            async () =>
                            {
                                result = await _relacionInstitucionRepository.GuardarPersonaRelacionInstitucion(dto);
                            }, new DbExceptionEvents
                            {
                                ForeignKeyViolation = () =>
                                    throw new ExcepcionOperativa(RelacionInstitucionEventos.RELACION_INSTITUCIONAL_ERROR_FK),
                                Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                            });
                    }

                    if (result == 0)
                    {
                        codigoEvento = RelacionInstitucionEventos.RELACION_INSTITUCIONAL_NO_GUARDADO;
                        codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                        throw new ExcepcionOperativa(codigoEvento);
                    }


                    _logger.Informativo($"AgregarPersonaRelacionInstitucion ");

                    // * auditoria
                    await _auditoria.AuditarAsync("PERS_PERSONAS_RELACION_INSTITUCION", dto);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"AgregarPersonaRelacionInstitucion => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"AgregarPersonaRelacionInstitucion => {ex}");
                        codigoEvento = RelacionInstitucionEventos.RELACION_INSTITUCIONAL_NO_GUARDADO;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = null //numeroRegistroDireccion
                };
            }
        }

        public async Task<Respuesta> ObtenerRelacionInstitucion(RelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                List<RelacionInstitucion> personarelacionInstitucion = new List<RelacionInstitucion>();
                string codigoEvento = RelacionInstitucionEventos.OBTENER_RELACIONES_INSTITUCIONALES;
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;

                try
                {
                    personarelacionInstitucion = await _relacionInstitucionRepository.ObtenerRelacionInstitucion(dto);
                    _logger.Informativo($"ObtenerPersonas => {codigoEvento}");
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonas => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonas => {ex}");
                        codigoEvento = RelacionInstitucionEventos.RELACIONES_INSTITUCIONALES_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = personarelacionInstitucion
                };
            }
        }

        public async Task<Respuesta> ObtenerRelacionInstitucionMin(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                PersonaRelacionInstitucion personarelacionInstitucion = new PersonaRelacionInstitucion();
                string codigoEvento =
                    RelacionInstitucionEventos.OBTENER_RELACION_INSTITUCIONAL; // Se obtuvieron los correos
                int codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;


                try
                {
                    personarelacionInstitucion =
                        await _relacionInstitucionRepository.ObtenerRelacionInstitucionMin(dto);
                    _logger.Informativo($"ObtenerPersonas => {codigoEvento}");
                }
                catch (Exception ex)
                {
                    if (ex is ExcepcionOperativa excOperativa)
                    {
                        _logger.Error(
                            $"ObtenerPersonas => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                        codigoEvento = ((ExcepcionOperativa)ex).codigoEvento;
                    }
                    else
                    {
                        _logger.Error($"ObtenerPersonas => {ex}");
                        codigoEvento = RelacionInstitucionEventos.RELACIONES_INSTITUCIONALES_NO_OBTENIDOS;
                    }

                    codigoRespuesta = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
                }

                string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                    _config.Idioma, codigoEvento, _config.Modulo);

                return new Respuesta()
                {
                    codigo = codigoRespuesta,
                    mensaje = textoInfo,
                    resultado = personarelacionInstitucion
                };
            }
        }
    }
}