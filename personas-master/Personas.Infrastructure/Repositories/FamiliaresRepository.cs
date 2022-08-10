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
using Personas.Core.Dtos.Familiares;
using Personas.Core.Entities.Familiares;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using Personas.Infrastructure.Querys.Familiares;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class FamiliaresRepository : IFamiliaresRepository
    {
        private readonly string _esquema;
        protected readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;
        protected readonly ILogsRepository<FamiliaresRepository> _logger;

        public FamiliaresRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            ILogsRepository<FamiliaresRepository> logger
        )
        {
            _config = config;
            _logger = logger;
            _conexionDb = conexionDb;
            _esquema = configuration["EsquemaDb"];
        }

        public async Task<int> GuardarFamiliar(GuardarFamiliarDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = FamiliaresQueries.GuardarFamiliar(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarFamiliar(ActualizarFamiliarDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = FamiliaresQueries.ActualizarFamiliar(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<IList<Familiar.FamiliarJoinMinimo>> ObtenerFamiliaresJoinMinimo(ObtenerFamiliaresDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = FamiliaresQueries.ObtenerFamiliaresJoin(_esquema);
                    var result = await _conexionDb.QueryAsync<Familiar.FamiliarJoinMinimo>(consulta, dto);
                    scope.Complete();

                    return result.ToList();
                }
                catch (Exception exc)
                {
                    // * TODO PORQUE REPRESENTANTES??
                    throw new ExcepcionOperativa(FamiliaresEventos.OBTENER_FAMILIARES_ERROR, exc);
                }
            }
        }

        public async Task<Familiar.FamiliarJoinFull> ObtenerFamiliarJoinFull(ObtenerFamiliarDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = FamiliaresQueries.ObtenerFamiliarJoin(_esquema);
                    Familiar.FamiliarJoinFull result =
                        await _conexionDb.QueryFirstOrDefaultAsync<Familiar.FamiliarJoinFull>(consulta, dto);
                    scope.Complete();

                    return result;
                }
                catch (Exception exc)
                {
                    // * TODO PORQUE REPRESENTANTES??
                    throw new ExcepcionOperativa(FamiliaresEventos.OBTENER_FAMILIAR_ERROR, exc);
                }
            }
        }

        public async Task<Familiar> ObtenerFamiliar(ObtenerFamiliarDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = FamiliaresQueries.ObtenerFamiliarEliminado(_esquema);
                    Familiar result = await _conexionDb.QueryFirstOrDefaultAsync<Familiar>(consulta, dto);
                    scope.Complete();

                    return result;
                }
                catch (Exception exc)
                {
                    // * TODO PORQUE REPRESENTANTES??
                    throw new ExcepcionOperativa(FamiliaresEventos.OBTENER_FAMILIAR_ERROR, exc);
                }
            }
        }

        public async Task<int> EliminarFamiliar(EliminarFamiliarDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = FamiliaresQueries.EliminarFamiliar(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public Task GuardarFamiliarHistorico(Familiar Familiar)
        {
            throw new NotImplementedException();
        }
    }
}