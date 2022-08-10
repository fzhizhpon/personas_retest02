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
    public class TipoDiscapacidadRepository : ITipoDiscapacidadRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public TipoDiscapacidadRepository(IConexion<OracleConnection> conexion, ILogger<TipoDiscapacidadRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectTiposDiscapacidades()
        {
            try
            {
                string keyRedis = "TiposDiscapacidades"; //LLave
                IEnumerable<ComboDto> discapacidades = null;
                discapacidades = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (discapacidades is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.TipoDiscapacidadQuery.SelectTiposDiscapacidades(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectTiposTiempo, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, discapacidades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectTiposTiempo, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
