using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.TelefonoMovil;
using Personas.Core.Entities.TelefonosMovil;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using VimaCoop.Auditoria.Interfaces;
using VimaCoop.DataBases.Entities;
using VimaCoop.DataBases.Interfaces;
using VimaCoop.Excepciones;

namespace Personas.Application.Services
{
    public class TelefonoMovilService : ITelefonoMovilService
    {
        protected readonly ITelefonoMovilRepository _telefonoMovilRepository;
        protected readonly IMensajesRespuestaRepository _textoInfoService;
        protected readonly ConfiguracionApp _config;
        protected readonly ILogsRepository<ReferenciasComercialesService> _logger;
        private readonly IDataBaseExceptions _dataBaseExceptions;
        private readonly IAuditoria _auditoria;

        public TelefonoMovilService(ITelefonoMovilRepository repository,
            IMensajesRespuestaRepository textoInfoService,
            ILogsRepository<ReferenciasComercialesService> logger,
            ConfiguracionApp config,
            IDataBaseExceptions dataBaseExceptions,
            IAuditoria auditoria
        )
        {
            _telefonoMovilRepository = repository;
            _textoInfoService = textoInfoService;
            _config = config;
            _logger = logger;
            _dataBaseExceptions = dataBaseExceptions;
            _auditoria = auditoria;
        }

        public async Task<Respuesta> GuardarTelefonoMovil(GuardarTelefonoMovilDto dto)
        {
            int codigo = 0;
            int resultado = 0;
            string codigoEvento = "";

            try
            {
                _logger.Informativo($"Existe telefono movil...");

                object telefonoMovil = await _telefonoMovilRepository.ObtenerTelefonoMovil(
                    new ObtenerTelefonoMovilDto()
                    {
                        codigoPersona = dto.codigoPersona,
                        numero = dto.numero
                    });

                if (telefonoMovil is not null)
                {
                    throw new ExcepcionOperativa(TelefonosFijosEventos.TELEFONO_FIJO_EXISTE);
                }

                _logger.Informativo($"Existio telefono movil...");

                _logger.Informativo($"Verificando telefono movil...");

                var resultadoVerificacion =
                    await _telefonoMovilRepository.esUnTelefonoMovillEliminado(
                        dto.codigoPersona, dto.numero);

                _logger.Informativo($"Verificado telefono movil...");


                if (resultadoVerificacion is not null)
                {
                    // * si existe previamente el telefono movil ingresada se lo va a volver a reactivar y modificar los campos


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () =>
                        {
                            resultado = await _telefonoMovilRepository.reactivarTelefonoMovil(new
                                {
                                    dto.codigoPersona,
                                    dto.numero,
                                    dto.codigoOperadora,
                                    dto.observaciones,
                                    dto.principal,
                                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                                    fechaUsuarioActualiza = DateTime.Now
                                }
                            );
                        },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TelefonosMovilesEventos.TELEFONO_MOVIL_ERROR_FK),
                            CheckConstraint = () =>
                                throw new ExcepcionOperativa(TelefonosMovilesEventos
                                    .TELEFONO_MOVIL_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });
                    codigoEvento = TelefonosMovilesEventos.GUARDAR_TELEFONO_MOVIL; // Se guardó telefono movil
                }
                else
                {
                    // * no existe previamente el telefono movil  ingresada se procede a guardar


                    await _dataBaseExceptions.CatchExceptionAsync(
                        async () => { (codigo, resultado) = await _telefonoMovilRepository.GuardarTelefonoMovil(dto); },
                        new DbExceptionEvents
                        {
                            ForeignKeyViolation = () =>
                                throw new ExcepcionOperativa(TelefonosMovilesEventos.TELEFONO_MOVIL_ERROR_FK),
                            CheckConstraint = () =>
                                throw new ExcepcionOperativa(TelefonosMovilesEventos
                                    .TELEFONO_MOVIL_ERROR_CHECK_CONSTRAINT),
                            Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                        });

                    codigoEvento = TelefonosMovilesEventos.GUARDAR_TELEFONO_MOVIL; // Se guardó telefono movil
                }


                if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
                {
                    if (resultado == 0)
                    {
                        codigoEvento =
                            TelefonosMovilesEventos.TELEFONO_MOVIL_NO_GUARDADO; // No se guardó telefono movil
                    }
                }
                else
                {
                    codigoEvento =
                        TelefonosMovilesEventos
                            .GUARDAR_TELEFONO_MOVIL_ERROR; // Ocurrio un error al guardar telefono movil
                }

                // * auditoria
                await _auditoria.AuditarAsync("PERS_TELEFONOS_MOVIL", dto);
            }
            catch (Exception exc)
            {
                codigo = CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;

                if (exc is ExcepcionOperativa excOperativa)
                {
                    _logger.Error(
                        $"GuardarReferenciaPersonal => {excOperativa.codigoEvento} - {excOperativa.InnerException}");
                    codigoEvento = excOperativa.codigoEvento;
                }
                else
                {
                    _logger.Error($"GuardarReferenciaPersonal => {exc}");
                    codigoEvento = ReferenciasPersonalesEventos.REFERENCIA_PERSONAL_NO_GUARDADO;
                }
            }


            _logger.Informativo($"GuardarTelefonoMovil => {codigoEvento}");

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigo,
                mensaje = textoInfo,
                resultado = null
            };
        }

        public async Task<Respuesta> ObtenerTelefonosMovil(ObtenerTelefonosMovilDto dto)
        {
            string codigoEvento = TelefonosMovilesEventos.OBTENER_TELEFONOS_MOVILES; // Se obtuvo telefonos moviles

            (int codigo, IEnumerable<TelefonoMovil> telefonos) =
                await _telefonoMovilRepository.ObtenerTelefonosMovil(dto);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (telefonos == null)
                {
                    codigoEvento =
                        TelefonosMovilesEventos.TELEFONOS_MOVILES_NO_OBTENIDOS; // No se encontró telefonos moviles
                }
            }
            else
            {
                codigoEvento =
                    TelefonosMovilesEventos
                        .OBTENER_TELEFONOS_MOVILES_ERROR; // Ocurrio un error al obtener telefonos moviles
            }


            _logger.Informativo($"ObtenerTelefonosMovil => {codigoEvento}");

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigo,
                mensaje = textoInfo,
                resultado = telefonos
            };
        }

        public async Task<Respuesta> ActualizarTelefonoMovil(ActualizarTelefonoMovilDto dto)
        {
            int codigo = 0;
            int resultado = 0;

            await _dataBaseExceptions.CatchExceptionAsync(
                async () => { (codigo, resultado) = await _telefonoMovilRepository.ActualizarTelefonoMovil(dto); },
                new DbExceptionEvents
                {
                    ForeignKeyViolation = () =>
                        throw new ExcepcionOperativa(TelefonosMovilesEventos.TELEFONO_MOVIL_ERROR_FK),
                    CheckConstraint = () =>
                        throw new ExcepcionOperativa(TelefonosMovilesEventos
                            .TELEFONO_MOVIL_ERROR_CHECK_CONSTRAINT),
                    Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                });


            string codigoEvento = TelefonosMovilesEventos.ACTUALIZAR_TELEFONO_MOVIL; // Se actilizó telefono movil

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (resultado == 0)
                {
                    codigoEvento =
                        TelefonosMovilesEventos.TELEFONO_MOVIL_NO_ACTUALIZADO; // No se actilizó telefono movil
                }
            }
            else
            {
                codigoEvento =
                    TelefonosMovilesEventos
                        .ACTUALIZAR_TELEFONO_MOVIL_ERROR; // Ocurrio un error al actilizar telefono movil
            }

            _logger.Informativo($"ActualizarTelefonoMovil => {codigoEvento}");

            // * auditoria
            await _auditoria.AuditarAsync("PERS_TELEFONOS_MOVIL", dto);

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigo,
                mensaje = textoInfo,
                resultado = null
            };
        }

        public async Task<Respuesta> EliminarTelefonoMovil(EliminarTelefonoMovilDto dto)
        {
            int codigo = 0;
            int resultado = 0;
            await _dataBaseExceptions.CatchExceptionAsync(
                async () => { (codigo, resultado) = await _telefonoMovilRepository.EliminarTelefonoMovil(dto); },
                new DbExceptionEvents
                {
                    ForeignKeyViolation = () =>
                        throw new ExcepcionOperativa(TelefonosMovilesEventos.TELEFONO_MOVIL_ERROR_FK),
                    CheckConstraint = () =>
                        throw new ExcepcionOperativa(TelefonosMovilesEventos
                            .TELEFONO_MOVIL_ERROR_CHECK_CONSTRAINT),
                    Default = (code, err) => throw new ExcepcionOperativa($"-0000-02-{code}", err)
                });

            string codigoEvento = TelefonosMovilesEventos.ELIMINAR_TELEFONO_MOVIL; // Se eliminó telefono movil

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (resultado == 0)
                {
                    codigoEvento = TelefonosMovilesEventos.TELEFONO_MOVIL_NO_ELIMINADO; // No se eliminó telefono movil
                }
            }
            else
            {
                codigoEvento =
                    TelefonosMovilesEventos
                        .ELIMINAR_TELEFONO_MOVIL_ERROR; // Ocurrio un error al eliminar telefono movil
            }

            // * auditoria
            await _auditoria.AuditarAsync("PERS_TELEFONOS_MOVIL", dto);

            _logger.Informativo($"EliminarTelefonoMovil => {codigoEvento}");

            string textoInfo = await _textoInfoService.ObtenerTextoInfo(
                _config.Idioma, codigoEvento, _config.Modulo);

            return new Respuesta()
            {
                codigo = codigo,
                mensaje = textoInfo,
                resultado = null
            };
        }
    }
}