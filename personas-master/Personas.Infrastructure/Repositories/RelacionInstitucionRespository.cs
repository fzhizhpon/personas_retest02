using Dapper;
using Microsoft.Extensions.Configuration;
using Personas.Application.CodigosEventos;
using Personas.Core.App;
using Personas.Core.Dtos.RelacionInstitucion;
using Personas.Core.Interfaces.IRepositories;
using Personas.Infrastructure.Querys.RelacionInstitucion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using VimaCoop.Excepciones;

namespace Personas.Infrastructure.Repositories
{
    public class RelacionInstitucionRepository : IRelacionInstitucionRepository
    {
        private IDbConnection _conexion;
        private readonly string _esquema;
        private readonly ILogsRepository<RelacionInstitucionRepository> _logger;
        private readonly ConfiguracionApp _config;

        public RelacionInstitucionRepository(IDbConnection conexion, IConfiguration configuration,
            ILogsRepository<RelacionInstitucionRepository> logger, ConfiguracionApp config)
        {
            _conexion = conexion;
            _esquema = configuration["EsquemaDb"];
            _logger = logger;
            _config = config;
        }


        public async Task<List<RelacionInstitucion>> ObtenerRelacionInstitucion(RelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }
                   ))
            {
                try
                {
                    string query = RelacionInstitucionQueries.obtenerData(_esquema);

                    List<RelacionInstitucion> relacionesInstitucion =
                        (await _conexion.QueryAsync<RelacionInstitucion>(query, dto)).ToList();

                    scope.Complete();

                    _logger.Informativo($"OK: ObtenerDirecciones => {relacionesInstitucion.Count}");
                    return relacionesInstitucion;
                }
                catch (Exception ex)
                {
                    _logger.Error($"ObtenerRelacionesInstitucion => {ex}");
                    throw new ExcepcionOperativa(RelacionInstitucionEventos.OBTENER_RELACIONES_INSTITUCIONALES_ERROR,
                        ex);
                }
            }
        }

        public async Task<int> ActualizarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }
                   ))
            {
                string query = RelacionInstitucionQueries.CambiarEstadoPersonaRelacionInstitucion(_esquema);
                int result = await _conexion.ExecuteAsync(query, dto);
                scope.Complete();
                return result;
            }
        }

        public async Task<int> GuardarPersonaRelacionInstitucion(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }
                   ))
            {
                string query = RelacionInstitucionQueries.InsertarPersonaRelacionInstitucion(_esquema);
                int result = await _conexion.ExecuteAsync(query, dto);
                scope.Complete();

                _logger.Informativo($"OK: AgregarPersonaRelacionInstitucion ");
                return result;
            }
        }

        public async Task<PersonaRelacionInstitucion> ObtenerRelacionInstitucionMin(PersonaRelacionInstitucion dto)
        {
            using (TransactionScope scope = new TransactionScope(
                       TransactionScopeOption.Required,
                       new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }
                   ))
            {
                try
                {
                    string consulta = RelacionInstitucionQueries.obtenerRelacionPersonaMin(_esquema);
                    PersonaRelacionInstitucion personaRelacionInstitucion =
                        await _conexion.QueryFirstOrDefaultAsync<PersonaRelacionInstitucion>(consulta, dto);

                    _logger.Informativo($"OK: ObtenerRelacionesInstitucion => {personaRelacionInstitucion}");
                    scope.Complete();
                    return personaRelacionInstitucion;
                }
                catch (Exception ex)
                {
                    _logger.Error($"ObtenerRelacionesInstitucion => {ex}");
                    throw new ExcepcionOperativa(RelacionInstitucionEventos.OBTENER_RELACION_INSTITUCIONAL_ERROR, ex);
                }
            }
        }
    }
}