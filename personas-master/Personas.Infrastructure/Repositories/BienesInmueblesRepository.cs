using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.BienesInmuebles;
using Personas.Core.Entities.BienesInmuebles;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.BienesInmuebles;
using VimaCoop.Excepciones;
using Vimasistem.QueryFilter.Interfaces;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Personas.Infrastructure.Repositories
{
    public class BienesInmueblesRepository : IBienesInmueblesRepository
    {
        // props
        private readonly string _esquema;
        private IDbConnection _connectionDb;
        private readonly IPagination _pagination;

        // constructor
        public BienesInmueblesRepository(IConfiguration configuration, IDbConnection connectionDb,
            IPagination pagination)
        {
            _esquema = configuration["EsquemaDb"];
            _connectionDb = connectionDb;
            _pagination = pagination;
        }

        public async Task<long> ObtenerNumeroRegistroMax()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesInmueblesQueries.ObtenerNumeroRegistroMax(_esquema);
                    long numeroRegistro = await _connectionDb.QueryFirstOrDefaultAsync<long>(consulta);
                    scope.Complete();
                    return numeroRegistro;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesInmueblesEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, ex);
                }
            }
        }

        public async Task<(int, IEnumerable<BienesInmuebles.Minimo>)> ObtenerBienesInmuebles(
            ObtenerBienesInmueblesDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string query = BienesInmueblesQueries.ObtenerBienesInmuebles(_esquema);
                    string[] tables = new[] { "PERS_BIENES_INMUEBLES:PBI" };
                    string filter = dto.GetQuery(tables);
                    if (!string.IsNullOrEmpty(filter))
                    {
                        query = query + " WHERE " + filter;
                    }

                    query += " " + _pagination.GetQuery(dto.indiceInicial, dto.numeroRegistros);
                    var resultados = await _connectionDb.QueryAsync<BienesInmuebles.Minimo>(query, dto);
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesInmueblesEventos.OBTENER_BIENES_INMUEBLES_ERROR, ex);
                }
            }
        }

        public async Task<(int, BienesInmuebles)> ObtenerBienInmueble(long codigoPersona,
            long numeroRegistro)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesInmueblesQueries.ObtenerBienInmueble(_esquema);
                    var resultados = await _connectionDb.QueryFirstOrDefaultAsync<BienesInmuebles>(consulta,
                        new { codigoPersona, numeroRegistro });
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesInmueblesEventos.OBTENER_BIEN_INMUEBLE_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarBienesInmuebles(GuardarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesInmueblesQueries.GuardarBienesInmuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> ActualizarBienesInmuebles(ActualizarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesInmueblesQueries.ActualizarBienesInmuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> EliminarBienesInmuebles(EliminarBienesInmueblesDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = BienesInmueblesQueries.EliminarBienesInmuebles(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<(int, IEnumerable<BienesInmuebles.MinimoSinJoin>)> ObtenerBienesInmueblesSinJoin(
            long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = BienesInmueblesQueries.ObtenerBienesInmueblesSinJoin(_esquema);
                    var result = await
                        _connectionDb.QueryAsync<BienesInmuebles.MinimoSinJoin>(consulta,
                            new { codigoPersona });
                    scope.Complete();
                    return (200, result);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(BienesInmueblesEventos.OBTENER_BIENES_INMUEBLES_ERROR, ex);
                }
            }
        }
    }
}