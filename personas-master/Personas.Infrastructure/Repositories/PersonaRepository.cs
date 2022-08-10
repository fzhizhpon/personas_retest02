using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.Personas;
using VimaCoop.Excepciones;
using Vimasistem.QueryFilter.Interfaces;

namespace Personas.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private IDbConnection _connection;
        private readonly string _esquema;
        private readonly IPagination _pagination;

        public PersonaRepository(IConfiguration configuration, IDbConnection connection, IPagination pagination)
        {
            _esquema = configuration["EsquemaDb"];
            _connection = connection;
            _pagination = pagination;
        }

        public async Task<int> ActualizarPersona(ActualizarPersonaDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = PersonaQuery.ActualizarPersona(_esquema);
                int result = await _connection.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<Persona> ObtenerPersona(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string consulta = PersonaQuery.ObtenerPersona(_esquema);
                    Persona result =
                        await _connection.QueryFirstOrDefaultAsync<Persona>(consulta,
                            new { codigoPersona = codigoPersona });

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasEventos.OBTENER_PERSONA_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarPersona(object dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = PersonaQuery.GuardarPersona(_esquema);
                int result = await _connection.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<long> ObtenerCodigoPersonaMax()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string consulta = PersonaQuery.ObtenerCodigoPersonaMax(_esquema);
                    long result = await _connection.QueryFirstOrDefaultAsync<long>(consulta);

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, ex);
                }
            }
        }

        public List<PersonaResponse> ObtenerPersonas(PersonaRequest persona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string query = PersonaQuery.ObtenerPersonas(_esquema);
                    string[] tables = new[] { "pers:pers", "PERS_PERSONAS:pp" };
                    string filter = persona.GetQuery(tables);
                    if (!string.IsNullOrEmpty(filter))
                    {
                        query = query + " WHERE " + filter;
                    }

                    query += " " + _pagination.GetQuery(persona.indiceInicial, persona.numeroRegistros);

                    IEnumerable<PersonaResponse> personas = _connection.Query<PersonaResponse>(query, persona);

                    return personas.ToList();
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasEventos.OBTENER_PERSONAS_ERROR, ex);
                }
            }
        }

        public async Task<Persona.PersonaJoinMinimo> ObtenerPersonaJoinMinimo(UltActPersonaRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = PersonaQuery.ObtenerPersonaJoinMinimo(_esquema);
                    Persona.PersonaJoinMinimo result =
                        await _connection.QueryFirstOrDefaultAsync<Persona.PersonaJoinMinimo>(consulta, dto);

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new ExcepcionOperativa(PersonasEventos.OBTENER_PERSONA_ERROR, ex);
                }
            }
        }

        public async Task ColocarFechaUltimaActualizacion(UltActPersonaRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                string query = PersonaQuery.ColocarFechaUltActualizacion(_esquema);
                await _connection.ExecuteAsync(query, dto);
                scope.Complete();
            }
        }

        public async Task<int> ObtenerPersonasPorIdentificacion(string nroIdentificacion)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string query = PersonaQuery.ContarPersonasIdentificacion(_esquema);
                    int numeroPersonas = await _connection.QueryFirstAsync<int>(query, new
                    {
                        nroIdentificacion = nroIdentificacion
                    });

                    scope.Complete();
                    return numeroPersonas;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasEventos.OBTENER_PERSONA_POR_ID_ERROR, ex);
                }
            }
        }
    }
}