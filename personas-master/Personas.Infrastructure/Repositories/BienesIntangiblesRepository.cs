using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Core.Dtos.BienesIntangibles;
using Personas.Core.Entities.BienesIntangibles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.BienesIntangibles;
using Personas.Application.CodigosEventos;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class BienesIntangiblesRepository : IBienesIntangiblesRepository
    {
        // props
        private readonly string _esquema;
        private IDbConnection _connectionDb;

        // constructor
        public BienesIntangiblesRepository(IConfiguration configuration, IDbConnection connectionDb)
        {
            _esquema = configuration["EsquemaDb"];
            _connectionDb = connectionDb;
        }


        public async Task<long> ObtenerNumeroRegistroMax()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesIntagiblesQueries.ObtenerNumeroRegistroMax(_esquema);
                    long numeroRegistro = await _connectionDb.QueryFirstOrDefaultAsync<long>(consulta);
                    scope.Complete();
                    return numeroRegistro;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesIntangiblesEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, ex);
                }
            }
        }

        public async Task<(int, IEnumerable<BienesIntangibles>)> ObtenerBienesIntangibles(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesIntagiblesQueries.ObtenerBienesIntangibles(_esquema);
                    var resultados = await _connectionDb.QueryAsync<BienesIntangibles>(consulta, new { codigoPersona });
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesIntangiblesEventos.OBTENER_BIENES_INTANGIBLES_ERROR, ex);
                }
            }
        }

        public async Task<(int, BienesIntangibles)> ObtenerBienIntangible(long codigoPersona,
            long numeroRegistro)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesIntagiblesQueries.ObtenerBienIntangible(_esquema);
                    var resultados = await _connectionDb.QueryFirstOrDefaultAsync<BienesIntangibles>(consulta,
                        new { codigoPersona, numeroRegistro });
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesIntangiblesEventos.OBTENER_BIEN_INTANGIBLE_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarBienesIntangibles(GuardarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesIntagiblesQueries.GuardarBienesIntagibles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> ActualizarBienesIntangibles(ActualizarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesIntagiblesQueries.ActualizarBienesIntangibles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> EliminarBienesIntangibles(EliminarBienesIntangiblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesIntagiblesQueries.EliminarBienesIntangibles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }
    }
}