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
    public class ParroquiaRepository : IParroquiaRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public ParroquiaRepository(IConexion<OracleConnection> conexion, ILogger<ParroquiaRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboDto>)> SelectParroquiasPorCiudad(ObtenerParroquiaCiudad dto)
        {
            try
            {
                string keyRedis = $"Parroquia-{dto.codigoPais}-{dto.codigoProvincia}-{dto.codigoCiudad}"; //LLave
                IEnumerable<ComboDto> parroquias = null;
                parroquias = _extensionCache.GetData<IEnumerable<ComboDto>>(keyRedis); //Recupera de REDIS

                // Si es null, vuelve a buscar en la db
                if (parroquias is null)
                {
                    using (var connection = _conexion.ObtenerConexion())
                    {
                        try
                        {
                            connection.Open();

                            string query = Queries.ParroquiaQuery.SelectParroquiasPorCiudad(_esquema);

                            var result = await connection.QueryAsync<ComboDto>(query, dto);

                            if (result.Any())
                                _extensionCache.SetData(result, keyRedis); // Se agrega a redis

                            return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ERROR: Error en el metodo SelectParroquiasPorCiudad, {ex}");
                            return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, parroquias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: Error en el metodo SelectParroquiasPorCiudad, {ex}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
            }
        }
    }
}
