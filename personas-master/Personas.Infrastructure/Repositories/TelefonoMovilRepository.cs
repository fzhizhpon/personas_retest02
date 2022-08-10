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
using Personas.Core.Dtos.TelefonoMovil;
using Personas.Core.Entities.TelefonosMovil;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.TelefonoMovil;
using VimaCoop.Excepciones;
using Vimasistem.QueryFilter.Interfaces;

namespace Personas.Infrastructure.Repositories
{
    public class TelefonoMovilRepository : ITelefonoMovilRepository
    {
        protected readonly IDbConnection _dbConnection;
        private readonly string _esquema;
        private readonly ConfiguracionApp _config;
        private readonly IPagination _pagination;
        private readonly ILogsRepository<ITelefonoMovilRepository> _logger;

        public TelefonoMovilRepository(
            IDbConnection dbConnection,
            IConfiguration configuration,
            ConfiguracionApp config,
            IPagination pagination,
            ILogsRepository<ITelefonoMovilRepository> logger
        )
        {
            _dbConnection = dbConnection;
            _esquema = configuration["EsquemaDb"];
            _config = config;
            _pagination = pagination;
            _logger = logger;
        }

        public async Task<(int, int)> GuardarTelefonoMovil(GuardarTelefonoMovilDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query;

                if (dto.principal == '1')
                {
                    query = TelefonoMovilQuery.QuitarTelefonoPrincipal(_esquema);
                    await _dbConnection.ExecuteAsync(query, new { codigoPersona = dto.codigoPersona });
                }
                else
                {
                    query = TelefonoMovilQuery.ContarTelefonosPrincipales(_esquema);
                    var registros =
                        await _dbConnection.QueryAsync<int>(query, new { codigoPersona = dto.codigoPersona });

                    if (registros.FirstOrDefault() == 0)
                    {
                        dto.principal = '1';
                    }
                }

                query = TelefonoMovilQuery.obtenerNuevoCodigo(_esquema);
                int codigo = (await _dbConnection.QueryAsync<int>(query, new { codigoPersona = dto.codigoPersona }))
                    .FirstOrDefault();

                codigo = codigo + 1;

                query = TelefonoMovilQuery.GuardarTelefonoMovil(_esquema);
                int result = await _dbConnection.ExecuteAsync(query, new
                {
                    codigoTelefonoMovil = codigo,
                    dto.codigoPersona,
                    dto.codigoPais,
                    dto.numero,
                    dto.codigoOperadora,
                    dto.observaciones,
                    dto.principal,
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                    fechaUsuarioActualiza = DateTime.Now
                });

                scope.Complete();
                _logger.Informativo($"OK: GuardarTelefonoMovilDto");

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
            }
        }

        public async Task<(int, IEnumerable<TelefonoMovil>)> ObtenerTelefonosMovil(ObtenerTelefonosMovilDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string consulta = TelefonoMovilQuery.ObtenerTelefonosMovil(_esquema);
                consulta = consulta + _pagination.GetQuery(dto.indiceInicial, dto.numeroRegistros);

                _logger.Informativo($"Consultando: ObtenerTelefonosMovil");

                var result = await _dbConnection.QueryAsync<TelefonoMovil>(consulta, dto);

                _logger.Informativo($"OK ObtenerTelefonosMovil");

                scope.Complete();
                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
            }
        }

        public async Task<object> ObtenerTelefonoMovil(ObtenerTelefonoMovilDto dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = TelefonoMovilQuery.ObtenerTelefonoMovil(_esquema);
                    object telefonoMovil = await _dbConnection.QueryFirstOrDefaultAsync(
                        consulta, dto
                    );
                    scope.Complete();
                    return telefonoMovil;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TelefonosMovilesEventos.OBTENER_TELEFONOS_MOVILES_ERROR, exc);
                }
            }
        }

        public async Task<(int, int)> ActualizarTelefonoMovil(ActualizarTelefonoMovilDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query = TelefonoMovilQuery.ActualizarTelefonoMovil(_esquema);

                int result = await _dbConnection.ExecuteAsync(query, new
                {
                    dto.codigoTelefonoMovil,
                    dto.codigoPersona,
                    dto.codigoOperadora,
                    dto.observaciones,
                    dto.principal,
                    codigoEstado = '1',
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                    fechaUsuarioActualiza = DateTime.Now
                });

                scope.Complete();
                _logger.Informativo($"OK: ActualizarTelefonoMovil => {result}");

                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
            }
        }

        public async Task<(int, int)> EliminarTelefonoMovil(EliminarTelefonoMovilDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }, TransactionScopeAsyncFlowOption.Enabled))
            {
                string query = TelefonoMovilQuery.EliminarTelefonoMovil(_esquema);

                int result = await _dbConnection.ExecuteAsync(query, new
                {
                    dto.codigoPersona,
                    dto.codigoTelefonoMovil,
                    fechaUsuarioActualiza = DateTime.Now,
                    codigoUsuarioActualiza = _config.codigoUsuarioRegistra,
                });

                scope.Complete();
                _logger.Informativo($"OK: EliminarTelefonoMovil => {result}");
                return (CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO, result);
            }
        }

        public async Task<object> esUnTelefonoMovillEliminado(int codigoPersona, string numero)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = TelefonoMovilQuery.esUnTelefonoMovilEliminado(_esquema);
                    var telefonoMovil =
                        await _dbConnection.QueryFirstOrDefaultAsync(consulta,
                            new { codigoPersona, numero });
                    scope.Complete();
                    return telefonoMovil;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<int> reactivarTelefonoMovil(object telefonoMovil)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TelefonoMovilQuery.reactivarTelefonoMovil(_esquema);
                int resultado = await _dbConnection.ExecuteAsync(consulta, telefonoMovil);
                scope.Complete();
                return resultado;
            }
        }
    }
}