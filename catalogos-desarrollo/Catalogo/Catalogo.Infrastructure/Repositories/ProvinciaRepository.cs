using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Provincia;
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
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public ProvinciaRepository(IConexion<OracleConnection> conexion, ILogger<ProvinciaRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, Provincia)> SelectProvincia(ObtenerProvinciaDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.ProvinciaQuery.SelectProvincia(_esquema);

                    var result = await connection.QueryAsync<Provincia>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectProvincia, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectProvinciasPorPais(ObtenerProvinciaPais dto)
        {
            try
            {
                string keyRedis = $"Provincia-{dto.codigoPais}"; //LLave
                IEnumerable<ComboDto> provincias = null;
                provincias = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis); //Recupera de REDIS

                // Si es null, vuelve a buscar en la db
                if (provincias is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.ProvinciaQuery.SelectProvinciasPorPais(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query, dto);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectProvinciasPorPais, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, provincias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectProvinciasPorPais, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
