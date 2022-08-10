using Catalogo.Core.DTOs;
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
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class OperadoraMovilRepository : IOperadoraMovilRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public OperadoraMovilRepository(IConexion<OracleConnection> conexion, ILogger<OperadoraMovilRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectOperadorasMovil()
        {
            try
            {
                string keyRedis = "OperadorasMovil"; //LLave
                IEnumerable<ComboDto> operadoras = null;
                operadoras = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (operadoras is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.OperadoraMovilQuery.SelectOperadorasMoviles(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectOperadorasMovil, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, operadoras);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectOperadorasMovil, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectPaisesMarcadoMovil()
        {
            try
            {
                string keyRedis = "PaisesMarcadoMovil"; //LLave
                IEnumerable<ComboDto> paisesMarcado = null;
                paisesMarcado = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (paisesMarcado is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.OperadoraMovilQuery.SelectPaisesMarcadoMoviles(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectPaisesMarcadoMovil, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, paisesMarcado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectPaisesMarcadoMovil, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
