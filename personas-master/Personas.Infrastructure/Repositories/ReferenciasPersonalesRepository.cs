using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using MongoDriver = MongoDB.Driver;
using Personas.Core.Dtos.ReferenciasPersonales;
using Personas.Core.Entities.ReferenciasPersonales;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using Personas.Infrastructure.Querys.ReferenciasPersonales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;
using Personas.Core.Dtos;
using Microsoft.Extensions.Options;

namespace Personas.Infrastructure.Repositories
{
    public class ReferenciasPersonalesRepository : IReferenciasPersonalesRepository
    {
        private readonly string _esquema;
        private readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;
        private readonly MongoDriver.MongoClient _mongoClient;
        protected readonly ILogsRepository<ReferenciasComercialesRepository> _logger;
        protected readonly IHistoricosRepository<ReferenciaPersonal> _historicosRepository;

        public ReferenciasPersonalesRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            IOptions<MongoOpciones> mongoConfig,
            ILogsRepository<ReferenciasComercialesRepository> logger,
            IHistoricosRepository<ReferenciaPersonal> historicosRepository
        )
        {
            _config = config;
            _logger = logger;
            _conexionDb = conexionDb;
            _esquema = configuration["EsquemaDb"];
            _historicosRepository = historicosRepository;

            MongoDriver.MongoClientSettings clientSettings =
                MongoDriver.MongoClientSettings.FromConnectionString(mongoConfig.Value.Connection);
            //// Timeout de mongo, configurado en Nacos por defecto a 3000 MS
            //clientSettings.ServerSelectionTimeout = TimeSpan.FromMilliseconds(3000);
            _mongoClient = new MongoDriver.MongoClient(clientSettings);
        }


        public async Task<int> GuardarReferenciaPersonal(GuardarReferenciaPersonalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = ReferenciasPersonalesQueries.guardarReferenciaPersonal(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);

                scope.Complete();
                return result;
            }
        }

        public async Task<ReferenciaPersonal> ObtenerReferenciaPersonal(ObtenerReferenciaPersonalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = ReferenciasPersonalesQueries.obtenerReferenciaPersonal(_esquema);
                    ReferenciaPersonal refPersonal =
                        await _conexionDb.QueryFirstOrDefaultAsync<ReferenciaPersonal>(consulta, dto);

                    scope.Complete();
                    return refPersonal;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<ReferenciaPersonal.ReferenciaPersonalJoin>
            ObtenerReferenciaPersonalJoin(ObtenerReferenciaPersonalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = ReferenciasPersonalesQueries.obtenerReferenciaPersonalJoin(_esquema);
                    ReferenciaPersonal.ReferenciaPersonalJoin refPersonalJoin =
                        await _conexionDb.QueryFirstOrDefaultAsync<ReferenciaPersonal.ReferenciaPersonalJoin>(consulta,
                            dto);

                    scope.Complete();
                    return refPersonalJoin;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<int> ObtenerCodigoReferenciaFinanciera(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = ReferenciasPersonalesQueries.obtenerNuevoCodigo(_esquema);
                    int result =
                        await _conexionDb.QueryFirstAsync<int>(consulta, new { codigoPersona = codigoPersona });
                    result++;

                    scope.Complete();

                    return result;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, exc);
                }
            }
        }

        public async Task<object> esUnaReferenciaPersonalEliminada(int codigoPersonaReferida,
            int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = ReferenciasPersonalesQueries.esUnaReferenciaPersonalEliminada(_esquema);
                    var referenciaPersonal =
                        await _conexionDb.QueryFirstOrDefaultAsync(consulta,
                            new { codigoPersonaReferida, codigoPersona });
                    scope.Complete();
                    return referenciaPersonal;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<int> reactivarReferenciaPersonal(ActualizarReferenciaPersonalDto referenciaPersonalDto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = ReferenciasPersonalesQueries.reactivarReferenciaPersonal(_esquema);
                int resultado = await _conexionDb.ExecuteAsync(consulta, referenciaPersonalDto);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<IList<ReferenciaPersonal.ReferenciaPersonalMinimo>>
            ObtenerReferenciasPersonales(ObtenerReferenciasPersonalesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = ReferenciasPersonalesQueries.obtenerReferenciasPersonales(_esquema);

                    var refPersonales = await _conexionDb
                        .QueryAsync<ReferenciaPersonal.ReferenciaPersonalMinimo>(consulta, new
                        {
                            codigoPersona = dto.codigoPersona,
                            indiceInicial = dto.paginacion.indiceInicial,
                            numeroRegistros = dto.paginacion.numeroRegistros,
                            estado = dto.estado
                        });

                    scope.Complete();
                    return refPersonales.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIAS_PERSONALES_ERROR,
                        exc);
                }
            }
        }

        public async Task<int> EliminarReferenciaPersonal(EliminarReferenciaPersonalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = ReferenciasPersonalesQueries.eliminarReferenciaPersonal(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public Task<int> ActualizarReferenciaPersonal(ActualizarReferenciaPersonalDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task GuardarReferenciaPersonalHistorico(ReferenciaPersonal refPersonal)
        {
            using (var session = await _mongoClient.StartSessionAsync())
            {
                try
                {
                    MongoDriver.IMongoDatabase db = _mongoClient.GetDatabase($"Historico_{_config.Modulo}");
                    MongoDriver.IMongoCollection<ReferenciaPersonal> coll =
                        db.GetCollection<ReferenciaPersonal>(ReferenciasPersonalesQueries.obtenerTabla());

                    session.StartTransaction();
                    await coll.InsertOneAsync(refPersonal);
                    await session.CommitTransactionAsync();
                }
                catch (Exception exc)
                {
                    await session.AbortTransactionAsync();
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.GUARDAR_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }
    }
}