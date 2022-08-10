using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos;
using Personas.Core.Dtos.Representantes;
using Personas.Core.Entities.Representantes;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.Representante;
using VimaCoop.Excepciones;
using Vimasistem.QueryFilter.Interfaces;
using IsolationLevel = System.Transactions.IsolationLevel;
using MongoDriver = MongoDB.Driver;

namespace Personas.Infrastructure.Repositories
{
    public class RepresentantesRepository : IRepresentantesRepository
    {
        private readonly string _esquema;
        protected readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;
        private readonly MongoDriver.MongoClient _mongoClient;
        protected readonly ILogsRepository<RepresentantesRepository> _logger;
        protected readonly IHistoricosRepository<Representante> _historicosRepository;
        private readonly IPagination _pagination;

        public RepresentantesRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            IOptions<MongoOpciones> mongoConfig,
            ILogsRepository<RepresentantesRepository> logger,
            IHistoricosRepository<Representante> historicosRepository,
            IPagination pagination
        )
        {
            _config = config;
            _logger = logger;
            _conexionDb = conexionDb;
            _esquema = configuration["EsquemaDb"];
            _historicosRepository = historicosRepository;
            _pagination = pagination;
            MongoDriver.MongoClientSettings clientSettings =
                MongoDriver.MongoClientSettings.FromConnectionString(mongoConfig.Value.Connection);
            //// Timeout de mongo, configurado en Nacos por defecto a 3000 MS
            //clientSettings.ServerSelectionTimeout = TimeSpan.FromMilliseconds(3000);
            _mongoClient = new MongoDriver.MongoClient(clientSettings);
        }

        public async Task<int> GuardarRepresentante(GuardarRepresentanteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = RepresentantesQueries.guardarRepresentante(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarRepresentante(ActualizarRepresentanteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = RepresentantesQueries.actualizarRepresentante(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> EliminarRepresentante(EliminarRepresentanteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = RepresentantesQueries.eliminarRepresentante(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public Task GuardarRepresentanteHistorico(Representante Representante)
        {
            throw new NotImplementedException();
        }

        public async Task<object> obtenerRepresentanteCodigoPersonaCodigoRepre(int codigoPersona,
            int codigoRepresentante)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.obtenerRepresentanteCodigoPersonaCodigoRepre(_esquema);
                    object trabajo = await _conexionDb.QueryFirstOrDefaultAsync(
                        consulta, new
                        {
                            codigoPersona,
                            codigoRepresentante
                        }
                    );
                    scope.Complete();
                    return trabajo;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<object> esUnRepresetanteEliminado(int codigoPersona, int codigoRepresentante)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.esUnRepresetanteEliminado(_esquema);
                    var trabajos =
                        await _conexionDb.QueryFirstOrDefaultAsync(consulta,
                            new { codigoPersona, codigoRepresentante });
                    scope.Complete();
                    return trabajos;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<int> reactivarRepresentante(object representante)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = RepresentantesQueries.reactivarRepresentante(_esquema);
                int resultado = await _conexionDb.ExecuteAsync(consulta, representante);
                scope.Complete();
                return resultado;
            }
        }

        public List<Representante.RepresentanteJoin> ObtenerRepresentantesFiltros(
            RepresentanteRequest representanteRequest)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string query = RepresentantesQueries.obtenerRepresentantesJoinFiltros(_esquema);
                    string[] tables = { "PERS_REPRESENTANTES:rep" };
                    string filter = representanteRequest.GetQuery(tables);

                    if (!string.IsNullOrEmpty(filter))
                    {
                        query = query + " AND " + filter;
                    }

                    query += " " + _pagination.GetQuery(representanteRequest.indiceInicial,
                        representanteRequest.numeroRegistros);

                    IEnumerable<Representante.RepresentanteJoin> result =
                        _conexionDb.Query<Representante.RepresentanteJoin>(query, representanteRequest);

                    scope.Complete();
                    return result.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(RepresentantesEventos.OBTENER_REPRESENTANTES_ERROR, exc);
                }
            }
        }

        public async Task<Representante.RepresentanteJoin> ObtenerRepresentante(ObtenerRepresentanteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.obtenerRepresentanteJoin(_esquema);
                    Representante.RepresentanteJoin result =
                        await _conexionDb.QueryFirstOrDefaultAsync<Representante.RepresentanteJoin>(consulta, dto);
                    scope.Complete();

                    return result;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(RepresentantesEventos.OBTENER_REPRESENTANTE_ERROR, exc);
                }
            }
        }

        public async Task<IList<Representante.RepresentanteJoin>> ObtenerRepresentantes(ObtenerRepresentantesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.obtenerRepresentantesJoin(_esquema);
                    var result = await _conexionDb.QueryAsync<Representante.RepresentanteJoin>(consulta, dto);
                    scope.Complete();

                    return result.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(RepresentantesEventos.OBTENER_REPRESENTANTES_ERROR, exc);
                }
            }
        }

        public async Task<IList<Representante.RepresentanteSimple>> ObtenerRepresentantesPrincipales(
            ObtenerRepresentantesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.obtenerRepresentantesPrincipales(_esquema);
                    var representantes = await _conexionDb.QueryAsync<Representante.RepresentanteSimple>(consulta, dto);
                    scope.Complete();

                    return representantes.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(RepresentantesEventos.OBTENER_REPRESENTANTES_PRINCIPALES_ERROR, exc);
                }
            }
        }

        public async Task<int> ActualizarRepresentantesPrincipales(IList<Representante.RepresentanteSimple> dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                string consulta = RepresentantesQueries.actualizarRepresentantePrincipal(_esquema);
                var representantes = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return representantes;
            }
        }

        public async Task<Representante.RepresentanteSimple> ObtenerRepresentanteMinimo(ObtenerRepresentanteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    string consulta = RepresentantesQueries.obtenerRepresentanteMinimo(_esquema);
                    Representante.RepresentanteSimple representante =
                        await _conexionDb.QueryFirstOrDefaultAsync<Representante.RepresentanteSimple>(consulta, dto);
                    scope.Complete();

                    return representante;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(RepresentantesEventos.OBTENER_REPRESENTANTE_ERROR, exc);
                }
            }
        }
    }
}