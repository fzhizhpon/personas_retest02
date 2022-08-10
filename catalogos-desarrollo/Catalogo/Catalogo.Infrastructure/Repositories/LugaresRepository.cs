using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Lugar;
using Catalogo.Core.Interfaces.DataBase;
using Catalogo.Core.Interfaces.IRepositories;
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
    public class LugaresRepository : ILugaresRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IPagination _pagination;
        private readonly string _esquema;

        public LugaresRepository(IConexion<OracleConnection> conexion, 
                                ILogger<LugaresRepository> logger,
                                IPagination pagination,
                                IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _pagination = pagination;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<LugarOutDto>)> SelectLugares(ObtenerLugaresDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.LugaresQueries.ObtenerLugares(_esquema);
                    query += (dto.GetQuery().Length > 0 ? " WHERE" : "") + dto.GetQuery();
                    query += " " + _pagination.GetQuery(dto.indiceInicial, dto.numeroRegistros);

                    Console.WriteLine(query);

                    var result = await connection.QueryAsync<LugarOutDto>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectLugares, {ex}");
                    return (CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO, null);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
