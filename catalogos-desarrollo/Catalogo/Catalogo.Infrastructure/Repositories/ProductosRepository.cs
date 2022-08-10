using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Producto;
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
    public class ProductosRepository : IProductosRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public ProductosRepository(IConexion<OracleConnection> conexion,
            ILogger<ProductosRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }


        public async Task<(int, IEnumerable<ComboDto>)> ObtenerProductosActividadFinanciera(
            ObtenerProductoActividadFinancieraDto dto)
        {
            try
            {
                string keyRedis = $"Productos-{dto.codigoMoneda}-{dto.codigoActividadFinanciera}"; //LLave
                IEnumerable<ComboDto> Productos = null;
                Productos = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis); //Recupera de REDIS

                // Si es null, vuelve a buscar en la db
                if (Productos is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();
                            string query =
                                Queries.ProductosQuery.ObtenerProductosActividadFinanciera(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query, dto);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo ObtenerProductosActividadFinanciera, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, Productos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo ObtenerProductosActividadFinanciera, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }

        public async Task<(int, IEnumerable<ComboDto>)> ObtenerProductosCodigoGrupo(
            ObtenerProductoCodigoGrupoDto dto)
        {
            try
            {
                string keyRedis = $"Productos-{dto.codigoMoneda}-{dto.codigoGrupo}"; //LLave
                IEnumerable<ComboDto> ProductosGrupo = null;
                ProductosGrupo =
                    _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis); //Recupera de REDIS

                // Si es null, vuelve a buscar en la db
                if (ProductosGrupo is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query =
                                Queries.ProductosQuery.ObtenerProductosCodigoGrupo(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query,dto);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo ObtenerProductosCodigoGrupo, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, ProductosGrupo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo ObtenerProductosCodigoGrupo, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}