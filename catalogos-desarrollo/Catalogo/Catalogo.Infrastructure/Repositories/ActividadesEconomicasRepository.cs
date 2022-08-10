using Catalogo.Core.DTOs;
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
using System.Text;
using System.Threading.Tasks;
using Vimasistem.QueryFilter.Interfaces;

namespace Catalogo.Infrastructure.Repositories
{
    public class ActividadesEconomicasRepository : IActividadesEconomicasRepository
    {
        private readonly string _esquema;
        private readonly ILogger _logger;
        private readonly IPagination _pagination;
        private readonly IConfiguration _configuration;
        private readonly IExtensionCache _extensionCache;
        private readonly IConexion<OracleConnection> _conexion;

        public ActividadesEconomicasRepository(IConexion<OracleConnection> conexion, ILogger<PaisRepository> logger,
            IExtensionCache extensionCache, IConfiguration configuration, IPagination pagination)
        {
            _conexion = conexion;
            _logger = logger;
            _extensionCache = extensionCache;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
            _pagination = pagination;
        }

        public async Task<(int, IEnumerable<ComboStringDto>)> ObtenerActividadesEconomicas(ActividadComercialDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    string query = Queries.ActividadEconomicaQueries.SelectActividadesEconomicas(_esquema);
                    string[] tables = new[] { "descripcion:pp" };
                    string filter = dto.GetQuery(tables);
                    if (!string.IsNullOrEmpty(filter))
                    {
                        query = query + " WHERE " + filter;
                    }

                    connection.Open();

                    query = query + _pagination.GetQuery(dto.indiceInicial, dto.numeroRegistros);

                    var result = await connection.QueryAsync<ComboStringDto>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo ObtenerActividadEconomica, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, new List<ComboStringDto>());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
