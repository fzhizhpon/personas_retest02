using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Profesion;
using Catalogo.Core.Entities;
using Catalogo.Core.Interfaces.DataBase;
using Catalogo.Core.Interfaces.IRepositories;
using CoopCrea.Cross.Cache.Src;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public ProfesionRepository(IConexion<OracleConnection> conexion, ILogger<ProfesionRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, int)> DeleteProfesion(int codigoProfesion)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = Queries.ProfesionQuery.DeleteProfesion(_esquema);

                        int result = await connection.ExecuteAsync(query, new
                        {
                            codigoProfesion = codigoProfesion
                        });

                        _extensionCache.DeleteData("Profesiones"); // Elimina datos de cahche

                        transaction.Commit();
                        _logger.LogInformation($"OK: DeleteProfesion => {result}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                    }
                    catch (Exception exc)
                    {
                        transaction.Rollback();
                        _logger.LogError($"ERROR: DeleteProfesion => {exc}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, -1);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<(int, int)> InsertProfesion(Profesion profesion)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = Queries.ProfesionQuery.InsertProfesion(_esquema);

                        int result = await connection.ExecuteAsync(query, profesion);

                        _extensionCache.DeleteData("Profesiones"); // Elimina datos de cahche

                        transaction.Commit();
                        _logger.LogInformation($"OK: InsertProfesion => {result}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                    }
                    catch (Exception exc)
                    {
                        transaction.Rollback();
                        _logger.LogError($"ERROR: InsertProfesion => {exc}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, -1);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<(int, Profesion)> SelectProfesion(int codigoProfesion)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.ProfesionQuery.SelectProfesion(_esquema);

                    var result = await connection.QueryAsync<Profesion>(query, new
                    {
                        codigoProfesion = codigoProfesion
                    });

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectProfesion, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectProfesiones()
        {
            try
            {
                string keyRedis = "Profesiones"; //LLave
                IEnumerable<ComboDto> profesiones = null;
                profesiones = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (profesiones is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.ProfesionQuery.SelectProfesiones(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectProfesiones, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, profesiones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectProfesiones, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }

        public async Task<(int, int)> UpdateProfesion(ActualizarProfesionDto profesion)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = Queries.ProfesionQuery.UpdateProfesion(_esquema);

                        int result = await connection.ExecuteAsync(query, profesion);

                        _extensionCache.DeleteData("Profesiones"); // Elimina datos de cahche

                        transaction.Commit();
                        _logger.LogInformation($"OK: UpdateProfesion => {result}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                    }
                    catch (Exception exc)
                    {
                        transaction.Rollback();
                        _logger.LogError($"ERROR: UpdateProfesion => {exc}");
                        return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, -1);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
