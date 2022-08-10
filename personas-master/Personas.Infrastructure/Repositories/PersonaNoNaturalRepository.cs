using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.Personas;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class PersonaNoNaturalRepository : IPersonaNoNaturalRepository
    {
        private readonly string _esquema;
        private IDbConnection _connection;

        public PersonaNoNaturalRepository(IConfiguration configuration, IDbConnection connectionDb)
        {
            _esquema = configuration["EsquemaDb"];
            _connection = connectionDb;
        }

        public async Task<int> GuardarPersonaNoNatural(object dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = PersonaNoNaturalQuery.GuardarPersonaNoNatural(_esquema);
                int result = await _connection.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<PersonaNoNatural> ObtenerPersonaNoNatural(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string consulta = PersonaNoNaturalQuery.ObtenerPersonaNoNatural(_esquema);
                    PersonaNoNatural result = await _connection
                        .QueryFirstOrDefaultAsync<PersonaNoNatural>(consulta,
                            new
                            {
                                codigoPersona = codigoPersona
                            });

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasNoNaturalesEventos.OBTENER_PERSONA_NO_NATURAL_ERROR, ex);
                }
            }
        }

        public async Task<int> ActualizarPersonaNoNatural(ActualizarPersonaNoNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = PersonaNoNaturalQuery.ActualizarPersonaNoNatural(_esquema);
                int result = await _connection.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }
    }
}