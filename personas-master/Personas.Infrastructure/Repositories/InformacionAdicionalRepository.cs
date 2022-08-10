using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.TablasComunes;
using Personas.Core.Entities.TablasComunes;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.TablasComunes;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class InformacionAdicionalRepository : IInformacionAdicionalRepository
    {
        // props
        private readonly string _esquema;
        private IDbConnection _connectionDb;

        public InformacionAdicionalRepository(IConfiguration configuration, IDbConnection connectionDb)
        {
            _esquema = configuration["EsquemaDb"];
            _connectionDb = connectionDb;
        }


        public async Task<(int, IEnumerable<InformacionAdicional>)> ObtenerInformacionAdicional(long codigoReferencia,
            long codigoTabla)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = InformacionAdicionalQuery.ObtenerInformacionAdicional(_esquema);
                    Console.WriteLine(consulta);
                    var resultados = await _connectionDb.QueryAsync<InformacionAdicional>
                        (consulta, new { codigoReferencia, codigoTabla });

                    Console.WriteLine(resultados);
                    scope.Complete();
                    return (200, resultados);
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(InformacionAdicionalEventos.OBTENER_INFORMACION_ADICIONAL_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarInformacionAdicional(GuardarInformacionAdicionalDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = InformacionAdicionalQuery.GuardarInformacionAdicional(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<int> ActualizarInformacionAdicional(ActualizarInformacionAdicionalDto obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = InformacionAdicionalQuery.ActualizarInformacionAdicional(_esquema);
                int resultado = await _connectionDb.ExecuteAsync(consulta, obj);
                scope.Complete();
                return resultado;
            }
        }
    }
}