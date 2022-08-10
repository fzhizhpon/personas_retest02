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
using Personas.Core.Dtos.Trabajos;
using Personas.Core.Entities.Trabajos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.Trabajos;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class TrabajosRepository : ITrabajosRepository
    {
        private readonly string _esquema;
        protected readonly ConfiguracionApp _config;
        protected readonly IDbConnection _conexionDb;
        protected readonly IHistoricosRepository<Trabajo> _historicosRepository;
        protected readonly ILogsRepository<ReferenciasComercialesRepository> _logger;

        public TrabajosRepository(
            ConfiguracionApp config,
            IDbConnection conexionDb,
            IConfiguration configuration,
            IHistoricosRepository<Trabajo> historicosRepository,
            ILogsRepository<ReferenciasComercialesRepository> logger
        )
        {
            _config = config;
            _logger = logger;
            _conexionDb = conexionDb;
            _esquema = configuration["EsquemaDb"];
            _historicosRepository = historicosRepository;
        }

        public async Task<int> GuardarTrabajo(int codigoTrabajo, GuardarTrabajoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TrabajosQueries.guardarTrabajo(_esquema);
                int result = await _conexionDb.ExecuteAsync(consulta, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ObtenerCodigoTrabajo(int codigoPersona)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = TrabajosQueries.obtenerNuevoCodigo(_esquema);
                    var result = await _conexionDb.QueryAsync<int>(consulta, new { codigoPersona = codigoPersona });

                    int codigo = 1;

                    if (result.FirstOrDefault() != 0) codigo = result.First();

                    scope.Complete();

                    return codigo;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TrabajosEventos.OBTENER_NUMERO_REGISTRO_MAX_ERROR, exc);
                }
            }
        }

        public async Task<object> obtenerTrabajoCodigoPersonaRazonSocial(int codigoPersona, string razonSocial)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = TrabajosQueries.obtenerTrabajoCodigoPersonaRazonSocial(_esquema);
                    object trabajo = await _conexionDb.QueryFirstOrDefaultAsync(
                        consulta, new
                        {
                            codigoPersona,
                            razonSocial
                        }
                    );
                    scope.Complete();
                    return trabajo;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TelefonosMovilesEventos.OBTENER_TELEFONOS_MOVILES_ERROR, exc);
                }
            }
        }

        public async Task<object> esUnTrabajoEliminado(int codigoPersona, string razonSocial)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                       TransactionScopeAsyncFlowOption.Enabled
                   ))
            {
                try
                {
                    string consulta = TrabajosQueries.esUnTrabajoEliminado(_esquema);
                    var trabajos =
                        await _conexionDb.QueryFirstOrDefaultAsync(consulta,
                            new { codigoPersona, razonSocial });
                    scope.Complete();
                    return trabajos;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(ReferenciasPersonalesEventos.OBTENER_REFERENCIA_PERSONAL_ERROR, exc);
                }
            }
        }

        public async Task<int> reactivarTrabajo(object trabajo)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string consulta = TrabajosQueries.reactivarTrabajo(_esquema);
                int resultado = await _conexionDb.ExecuteAsync(consulta, trabajo);
                scope.Complete();
                return resultado;
            }
        }

        public async Task<Trabajo> ObtenerTrabajo(ObtenerTrabajoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = TrabajosQueries.obtenerTrabajo(_esquema);
                    Trabajo trabajo = await _conexionDb.QueryFirstOrDefaultAsync<Trabajo>(consulta, dto);

                    scope.Complete();
                    return trabajo;
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TrabajosEventos.OBTENER_TRABAJO_ERROR, exc);
                }
            }
        }

        public async Task<IList<Trabajo.TrabajoMinimo>> ObtenerTrabajos(ObtenerTrabajosDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                try
                {
                    string consulta = TrabajosQueries.obtenerTrabajos(_esquema);

                    var trabajos = await _conexionDb
                        .QueryAsync<Trabajo.TrabajoMinimo>(consulta, new
                        {
                            codigoPersona = dto.codigoPersona,
                            indiceInicial = dto.indiceInicial,
                            numeroRegistros = dto.numeroRegistros
                        });

                    scope.Complete();
                    return trabajos.ToList();
                }
                catch (Exception exc)
                {
                    throw new ExcepcionOperativa(TrabajosEventos.OBTENER_TRABAJOS_ERROR, exc);
                }
            }
        }

        public async Task<int> EliminarTrabajo(EliminarTrabajoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = TrabajosQueries.eliminarTrabajo(_esquema);
                int result = await _conexionDb.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> ActualizarTrabajo(ActualizarTrabajoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                   }))
            {
                string query = TrabajosQueries.actualizarTrabajo(_esquema);
                int result = await _conexionDb.ExecuteAsync(query, dto);

                scope.Complete();
                return result;
            }
        }
    }
}