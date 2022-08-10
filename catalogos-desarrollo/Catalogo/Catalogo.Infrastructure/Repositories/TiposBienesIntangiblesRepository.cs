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
    public class TiposBienesIntangiblesRepository : ITiposBienesIntangiblesRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public TiposBienesIntangiblesRepository(IConexion<OracleConnection> conexion,
            ILogger<TiposBienesIntangiblesRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboDto>)> ObtenerTiposBienesIntangibles()
        {
            try
            {
                string keyRedis = "TiposBienesIntangibles"; // llave
                IEnumerable<ComboDto> tiposBienesIntangibles = null;
                tiposBienesIntangibles = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                if (tiposBienesIntangibles is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.TiposBienesIntangiblesQuery.ObtenerTiposBienesIntangibles(_esquema);
                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                            {
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis
                            }

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo ObtenerTiposBienesIntangibles, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, tiposBienesIntangibles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo ObtenerTiposBienesIntangibles, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}