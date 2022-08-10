using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.DataBase;
using Catalogo.Core.Interfaces.IRepositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoopCrea.Cross.Cache.Src;

namespace Catalogo.Infrastructure.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public AgenciaRepository(IConexion<OracleConnection> conexion, ILogger<AgenciaRepository> logger,
            IConfiguration configuration, IExtensionCache extensionCaches)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCaches;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectAgenciasPorSucursal(ObtenerAgenciaSucursalDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.AgenciaQuery.SelectAgenciasPorSucursal(_esquema);

                    var result = await connection.QueryAsync<ComboDto>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectAgenciasPorSucursal, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectAgencias()
        {
            try
            {
                string keyRedis = "Agencias"; //LLave
                IEnumerable<ComboDto> agencias;
                Console.WriteLine(_extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis));
                agencias = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (agencias is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.AgenciaQuery.SelectAgencias(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectSucursales, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, agencias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectAgencias, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}