using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.Dtos.Personas;
using Personas.Core.Entities.Personas;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.Personas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class PersonaNaturalRepository : IPersonaNaturalRepository
    {
        private readonly string _esquema;
        private IDbConnection _connectionDb;

        public PersonaNaturalRepository(IConfiguration configuration, IDbConnection connectionDb)
        {
            _esquema = configuration["EsquemaDb"];
            _connectionDb = connectionDb;
        }

        public async Task<InfoPersonaNaturalDto> ObtenerInfoPersona(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string consulta = PersonaNaturalQuery.InfoPersona(_esquema);
                    InfoPersonaNaturalDto result =
                        await _connectionDb.QueryFirstOrDefaultAsync<InfoPersonaNaturalDto>(consulta,
                            new { codigoPersona = codigoPersona });

                    consulta = PersonaNaturalQuery.FotoCedula(_esquema);

                    List<string> fotoIdentificacion =
                        (await _connectionDb.QueryAsync<string>(consulta, new { codigoPersona = codigoPersona }))
                        .ToList();

                    if (fotoIdentificacion.Count > 0)
                    {
                        result.fotoIdentificacion = new FotoIdentificacion();

                        result.fotoIdentificacion.frontal = fotoIdentificacion[0];

                        if (fotoIdentificacion.Count > 1) result.fotoIdentificacion.posterior = fotoIdentificacion[1];
                    }

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasNaturalesEventos.OBTENER_INFORMACION_PERSONA_ERROR, ex);
                }
            }
        }

        public async Task<int> GuardarPersonaNatural(object dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query = PersonaNaturalQuery.GuardarPersonaNatural(_esquema);
                int result = await _connectionDb.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarPersonaNatural(ActualizarPersonaNaturalDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query = PersonaNaturalQuery.ActualizarPersonaNatural(_esquema);
                int result = await _connectionDb.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<PersonaNatural> ObtenerPersonaNatural(long codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string consulta = PersonaNaturalQuery.ObtenerPersonaNatural(_esquema);
                    PersonaNatural result =
                        await _connectionDb.QueryFirstOrDefaultAsync<PersonaNatural>(consulta,
                            new { codigoPersona = codigoPersona });

                    scope.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ExcepcionOperativa(PersonasNaturalesEventos.OBTENER_PERSONA_NATURAL_ERROR, ex);
                }
            }
        }

        public async Task<int> ActualizarConyugue(ActualizarConyugueRequest dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query = PersonaNaturalQuery.QuitarConyuge(_esquema);
                await _connectionDb.ExecuteAsync(query, new
                {
                    codigoPersona = dto.codigoPersona
                });
                query = PersonaNaturalQuery.ActualizarDatosConyuge(_esquema);
                int result = await _connectionDb.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }
    }
}