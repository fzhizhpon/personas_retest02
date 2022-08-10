using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.EnumsEventos;
using Personas.Core.App;
using Personas.Core.Dtos.Direcciones;
using Personas.Core.Entities.Direcciones;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using Personas.Infrastructure.Querys.Direcciones;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class DireccionesRepository : IDireccionesRepository
    {
        private IDbConnection _conexion;
        private readonly string _esquema;
        private readonly ILogsRepository<DireccionesRepository> _logger;
        private readonly ConfiguracionApp _config;

        public DireccionesRepository(IDbConnection conexion, IConfiguration configuration,
            ILogsRepository<DireccionesRepository> logger, ConfiguracionApp config)
        {
            _conexion = conexion;
            _esquema = configuration["EsquemaDb"];
            _logger = logger;
            _config = config;
        }

        public async Task<int> ObtenerCodigoNuevaDireccion(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = DireccionesQueries.ObtenerCodigoNuevaDireccion(_esquema);
                    int codigoCorreo =
                        await _conexion.QueryFirstAsync<int>(query, new { codigoPersona = codigoPersona });
                    codigoCorreo += 1;

                    scope.Complete();
                    return codigoCorreo;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(DireccionesEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarDireccion(GuardarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string query = DireccionesQueries.InsertarDireccion(_esquema);

                int filasAfectadas = await _conexion.ExecuteAsync(query, new
                {
                    numeroRegistro = dto.numeroRegistro,
                    codigoPersona = dto.codigoPersona,
                    codigoPais = dto.codigoPais,
                    codigoProvincia = dto.codigoProvincia,
                    codigoCiudad = dto.codigoCiudad,
                    codigoParroquia = dto.codigoParroquia,
                    callePrincipal = dto.callePrincipal,
                    calleSecundaria = dto.calleSecundaria,
                    numeroCasa = dto.numeroCasa,
                    sector = dto.sector,
                    codigoPostal = dto.codigoPostal,
                    esMarginal = dto.esMarginal,
                    latitud = dto.latitud,
                    longitud = dto.longitud,
                    principal = dto.principal,
                    codigoTipoResidencia = dto.codigoTipoResidencia,
                    comunidad = dto.comunidad,
                    referencia = dto.referencia,
                    tipoSector = dto.tipoSector,
                    fechaInicialResidencia = dto.fechaInicialResidencia,
                    valorArriendo = dto.valorArriendo,
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                    fechaUsuarioActualiza = DateTime.Now
                });

                scope.Complete();

                _logger.Informativo($"OK: AgregarDireccion");
                return filasAfectadas;
            }
        }

        public async Task<int> DesmarcarDireccionPrincipal(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = DireccionesQueries.QuitarPrincipal(_esquema);

                    int filasAfectadas = await _conexion.ExecuteAsync(query, new { codigoPersona = codigoPersona });

                    scope.Complete();
                    return filasAfectadas;
                }
                catch (Exception ex)
                {
                    _logger.Error($"DesmarcarPrincipal => {ex}");
                    throw new ExcepcionOperativa(DireccionesEventos.DESMARCAR_DIRECCION_PRINCIPAL_ERROR, ex);
                }
            }
        }

        public async Task<int> NroDireccionesPrincipales(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = DireccionesQueries.ContarDireccionesPrincipales(_esquema);
                    int registros = await _conexion.QueryFirstAsync<int>(query, new { codigoPersona = codigoPersona });

                    scope.Complete();
                    return registros;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(DireccionesEventos.NUMERO_DIRECCIONES_PRINCIPÁLES, ex);
                }
            }
        }

        public async Task<int> EliminarDireccion(EliminarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string query = DireccionesQueries.EliminarDireccion(_esquema);
                int filasAfectadas = await _conexion.ExecuteAsync(query, new
                {
                    codigoPersona = dto.codigoPersona,
                    numeroRegistro = dto.numeroRegistro,
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                    fechaActualiza = DateTime.Now
                });

                scope.Complete();
                return filasAfectadas;
            }
        }

        public async Task<List<DireccionMinResponse>> ObtenerDirecciones(ObtenerDireccionesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = DireccionesQueries.ObtenerDirecciones(_esquema);

                    List<DireccionMinResponse> direcciones =
                        (await _conexion.QueryAsync<DireccionMinResponse>(query, dto)).ToList();

                    scope.Complete();

                    _logger.Informativo($"OK: ObtenerDirecciones => {direcciones.Count}");
                    return direcciones;
                }
                catch (Exception ex)
                {
                    _logger.Error($"ObtenerDirecciones => {ex}");
                    throw new ExcepcionOperativa(DireccionesEventos.OBTENER_DIRECCIONES_ERROR, ex);
                }
            }
        }

        public async Task<DireccionOutDto> ObtenerDireccion(ObtenerDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = DireccionesQueries.ObtenerDireccion(_esquema);

                    //DireccionOutDto direccion = await _conexion.QueryFirstAsync<DireccionOutDto>(query, dto);

                    var response =
                        await _conexion.QueryAsync<DireccionOutDto, Direccion, TelefonoFijoOutDto, DireccionOutDto>(
                            query,
                            map: (outDto, dir, tel) =>
                            {
                                outDto.direccion = dir;
                                outDto.telefonoFijo = tel;
                                return outDto;
                            },
                            splitOn: "codigoPersona,numeroRegistro",
                            param: dto);

                    scope.Complete();

                    return response.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    _logger.Error($"ObtenerDireccion => {ex}");
                    throw new ExcepcionOperativa(DireccionesEventos.OBTENER_DIRECCION_ERROR, ex);
                }
            }
        }

        public async Task<int> ActualizarDireccion(ActualizarDireccionDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string query = DireccionesQueries.ActualizarDireccion(_esquema);

                int filasAfectadas = await _conexion.ExecuteAsync(query, new
                {
                    numeroRegistro = dto.numeroRegistro,
                    codigoPersona = dto.codigoPersona,
                    codigoPais = dto.codigoPais,
                    codigoProvincia = dto.codigoProvincia,
                    codigoCiudad = dto.codigoCiudad,
                    codigoParroquia = dto.codigoParroquia,
                    callePrincipal = dto.callePrincipal,
                    calleSecundaria = dto.calleSecundaria,
                    numeroCasa = dto.numeroCasa,
                    sector = dto.sector,
                    codigoPostal = dto.codigoPostal,
                    esMarginal = dto.esMarginal,
                    latitud = dto.latitud,
                    longitud = dto.longitud,
                    codigoRegistroCivil = dto.codigoRegistroCivil,
                    principal = dto.principal,
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                    fechaUsuarioActualiza = DateTime.Now,
                    codigoTipoResidencia = dto.codigoTipoResidencia,
                    comunidad = dto.comunidad,
                    referencia = dto.referencia,
                    tipoSector = dto.tipoSector,
                    fechaInicialResidencia = dto.fechaInicialResidencia,
                    valorArriendo = dto.valorArriendo
                });

                scope.Complete();

                _logger.Informativo($"OK: ActualizarDireccion => {dto.numeroRegistro}");
                return filasAfectadas;
            }
        }
    }
}