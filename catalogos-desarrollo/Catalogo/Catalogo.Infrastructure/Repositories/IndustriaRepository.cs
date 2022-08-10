using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.DataBase;
using Catalogo.Core.Interfaces.IRepositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class IndustriaRepository : IIndustriaRepository
    {
        private readonly IConexion<OracleConnection> _conexion;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly string _esquema;

        public IndustriaRepository(IConexion<OracleConnection> conexion, ILogger<IndustriaRepository> logger,
            IConfiguration configuration)
        {
            _conexion = conexion;
            _logger = logger;
            _configuration = configuration;
            _esquema = _configuration["EsquemaDb"];
        }

        public async Task<(int, IEnumerable<ComboStringDto>)> SelectIndustriasPorSubSectorEconomico(ObtenerIndustriaDto dto)
        {
            using (var connection = _conexion.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = Queries.IndustriaQuery.SelectIndustriaPorSubSector(_esquema);

                    var result = await connection.QueryAsync<ComboStringDto>(query, dto);

                    return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: Error en el metodo SelectIndustriasPorSubSectorEconomico, {ex}");
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
