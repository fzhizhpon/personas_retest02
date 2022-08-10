using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.EstadosFinancieros;
using Personas.Core.Entities.EstadosFinancieros;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using Personas.Infrastructure.Querys.EstadosFinancieros;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class EstadosFinancierosRepository : IEstadosFinancierosRepository
    {
        private readonly string _esquema;
        protected readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;

        protected readonly ILogsRepository<EstadosFinancierosRepository> _logger;
        //protected readonly IHistoricosRepository<Familiar> _historicosRepository;

        public EstadosFinancierosRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            ILogsRepository<EstadosFinancierosRepository> logger
            //IHistoricosRepository<Familiar> historicosRepository
        )
        {
            _config = config;
            _logger = logger;
            _conexionDb = conexionDb;
            _esquema = configuration["EsquemaDb"];
            //_historicosRepository = historicosRepository;
        }

        public async Task<int> GuardarEstadosFinancieros(GuardarEstadoFinancieroDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = EstadosFinancierosQueries.GuardarEstadoFinanciero(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarEstadosFinancieros(ActualizarEstadoFinancieroDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = EstadosFinancierosQueries.ActualizarEstadoFinanciero(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<List<EstadoFinanciero>> ObtenerCuentasEstadosFinancieros(
            ObtenerCuentasEstadoFinancieroDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = EstadosFinancierosQueries.ObtenerCuentasEstadoFinanciero(_esquema);
                    var result = await _conexionDb.QueryAsync<EstadoFinanciero>(consulta, dto);
                    scope.Complete();

                    return result.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(EstadosFinancierosEventos.OBTENER_ESTADOS_FINANCIEROS_ERROR, exc);
                }
            }
        }

        public async Task<double> ObtenerValorCuentaPorQuery(string query, int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    double valor = await _conexionDb.QueryFirstAsync<double>(query, new
                    {
                        codigoPersona = codigoPersona
                    });

                    scope.Complete();
                    return valor;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(EstadosFinancierosEventos.OBTENER_VALOR_CUENTA_ERROR, exc);
                }
            }
        }
    }
}