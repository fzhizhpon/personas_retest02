using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Transactions;
using Dapper;
using Personas.Core.Dtos.BienesMuebles;
using Personas.Core.Entities.BienesMuebles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.BienesMuebles;
using Personas.Application.CodigosEventos;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class BienesMueblesRepository : IBienesMueblesRepository
    {
        // props
        private readonly string _esquema;
        private IDbConnection _connectionDb;

        // constructor
        public BienesMueblesRepository(IConfiguration configuration, IDbConnection connectionDb)
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
                    string consulta = BienesMueblesQueries.ObtenerNumeroRegistroMax(_esquema);
                    long numeroRegistro = await _connectionDb.QueryFirstOrDefaultAsync<long>(consulta);
                    scope.Complete();
                    return numeroRegistro;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesMueblesEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, ex);
                }
            }
        }

        public async Task<(int, IEnumerable<BienesMuebles>)> ObtenerBienesMuebles(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesMueblesQueries.ObtenerBienesMuebles(_esquema);
                    var resultados = await _connectionDb.QueryAsync<BienesMuebles>(consulta, new { codigoPersona });
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesMueblesEventos.OBTENER_BIENES_MUEBLES_ERROR, ex);
                }
            }
        }

        public async Task<(int, BienesMuebles)> ObtenerBienMueble(long codigoPersona, long numeroRegistro)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesMueblesQueries.ObtenerBienMueble(_esquema);
                    var resultados = await _connectionDb.QueryFirstOrDefaultAsync<BienesMuebles>(consulta,
                        new { codigoPersona, numeroRegistro });
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesMueblesEventos.OBTENER_BIEN_MUEBLE_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarBienesMuebles(GuardarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesMueblesQueries.GuardarBienesMuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> ActualizarBienesMuebles(ActualizarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesMueblesQueries.ActualizarBienesMuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> EliminarBienesMuebles(EliminarBienesMueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesMueblesQueries.EliminarBienesMuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }
    }
}