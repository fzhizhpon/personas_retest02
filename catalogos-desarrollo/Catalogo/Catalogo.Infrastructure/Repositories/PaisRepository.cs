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
using Vimasistem.QueryFilter.Interfaces;

namespace Catalogo.Infrastructure.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;
        private readonly IPagination _pagination;

        public PaisRepository(IConexion<OracleConnection> conexion, ILogger<PaisRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration, IPagination pagination)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
            _pagination = pagination;
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectPaises()
        {
            try
            {
                string keyRedis = "Pais"; //LLave
                IEnumerable<ComboDto> paises = null;
                paises = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis); //Recupera de REDIS

                // Si es null, vuelve a buscar en la db
                if (paises is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.PaisQuery.SelectPaises(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectPaises, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, paises);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectPaises, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectPaises(PaginacionDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.PaisQuery.SelectPaisesPaginacion(_esquema);
                    query = query + _pagination.GetQuery(dto.indiceInicial, dto.numeroRegistros);

                    var result = await connection.QueryAsync<ComboDto>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectPaisesPaginacion, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, new List<ComboDto>());
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
