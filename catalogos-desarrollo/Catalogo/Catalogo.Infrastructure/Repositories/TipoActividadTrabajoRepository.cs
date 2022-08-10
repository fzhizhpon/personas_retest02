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
    public class TipoActividadTrabajoRepository : ITipoActividadTrabajoRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public TipoActividadTrabajoRepository(IConexion<OracleConnection> conexion, ILogger<TipoActividadTrabajoRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboStringDto>)> SelectTiposActividadesTrabajo()
        {
            try
            {
                string keyRedis = "TiposActividadesTrabajos"; //LLave
                IEnumerable<ComboStringDto> tiposActividades = null;
                tiposActividades = _extensionCache.GetData<IEnumerable<ComboStringDto>>(keyRedis);

                // Si es null, vuelve a buscar en la db
                if (tiposActividades is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.TipoActividadTrabajoQuery.SelectTiposActividadesTrabajos(_esquema);

                            var result = await connection.QueryAsync<ComboStringDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectTiposActividadesTrabajo, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, tiposActividades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectTiposActividadesTrabajo, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
