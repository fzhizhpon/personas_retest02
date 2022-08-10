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
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class CaracteristicaCuentaRepository : ICaracteristicaCuentaRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public CaracteristicaCuentaRepository(IConexion<OracleConnection> conexion, ILogger<CaracteristicaCuentaRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboStringDto>)> SelectCaracteristicaCuentas()
        {
            try
            {
                string keyRedis = "CaracteristicaCuentas"; //LLave
                IEnumerable<ComboStringDto> CaracteristicaCuentas = null;
                CaracteristicaCuentas = _extensionCache.GetData<IEnumerable<ComboStringDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (CaracteristicaCuentas is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.CaracteristicaCuentaQuery.SelectCaracteristicaCuenta(_esquema);

                            var result = await connection.QueryAsync<ComboStringDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectCaracteristicaCuenta, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, CaracteristicaCuentas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectCaracteristicaCuentas, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
