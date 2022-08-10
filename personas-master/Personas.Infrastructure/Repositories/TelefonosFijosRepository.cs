using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos;
using Personas.Core.Dtos.TelefonosFijos;
using Personas.Core.Entities.TelefonosFijos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using Personas.Infrastructure.Querys.TelefonosFijos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;
using MongoDriver = MongoDB.Driver;

namespace Personas.Infrastructure.Repositories
{
    public class TelefonosFijosRepository : ITelefonosFijosRepository
    {
        private readonly string _esquema;
        protected readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;
        private readonly MongoDriver.MongoClient _mongoClient;
        protected readonly IHistoricosRepository<TelefonoFijo> _historicosRepository;
        protected readonly ILogsRepository<TelefonosFijosRepository> _logger;

        public TelefonosFijosRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            IOptions<MongoOpciones> mongoConfig,
            IHistoricosRepository<TelefonoFijo> historicosRepository,
            ILogsRepository<TelefonosFijosRepository> logger
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

        public async Task<int> GuardarTelefonoFijo(GuardarTelefonoFijoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TelefonosFijosQueries.GuardarTelefonoFijo(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarTelefonoFijo(ActualizarTelefonoFijoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TelefonosFijosQueries.ActualizarTelefonoFijo(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> EliminarTelefonoFijo(EliminarTelefonoFijoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TelefonosFijosQueries.EliminarTelefonoFijo(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<IList<TelefonoFijo.TelefonoFijoMinimo>> ObtenerTelefonosFijos(ObtenerTelefonosFijosDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = TelefonosFijosQueries.ObtenerTelefonosFijos(_esquema);

                    var telefonosFIjos = await _conexionDb
                        .QueryAsync<TelefonoFijo.TelefonoFijoMinimo>(consulta, dto);

                    scope.Complete();
                    return telefonosFIjos.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TelefonosFijosEventos.OBTENER_TELEFONOS_FIJOS_ERROR, exc);
                }
            }
        }

        public async Task<TelefonoFijo.TelefonoFijoFull> ObtenerTelefonoFijo(ObtenerTelefonoFijoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = TelefonosFijosQueries.ObtenerTelefonoFijo(_esquema);

                    TelefonoFijo.TelefonoFijoFull telefonoFIjo = await _conexionDb
                        .QueryFirstOrDefaultAsync<TelefonoFijo.TelefonoFijoFull>(consulta, dto);

                    scope.Complete();

                    return telefonoFIjo;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TelefonosFijosEventos.OBTENER_TELEFONO_FIJO_ERROR, exc);
                }
            }
        }

        public async Task<int> GenerarNumeroRegistro(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = TelefonosFijosQueries.ObtenerCountUltimoNumeroRegistro(_esquema);
                    int result = await _conexionDb.QueryFirstOrDefaultAsync<int>(consulta, new { codigoPersona });
                    result++;
                    scope.Complete();

                    return result;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TelefonosFijosEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, exc);
                }
            }
        }
    }
}