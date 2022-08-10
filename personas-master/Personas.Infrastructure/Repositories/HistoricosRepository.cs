using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personas.Core.Dtos;
using Personas.Core.Interfaces.IRepositories;
using Personas.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Repositories
{
    public class HistoricosRepository<T> : IHistoricosRepository<T>
    {

        protected readonly ILogsRepository<HistoricosRepository<T>> _logger;

        public MongoClient _mongoClient { get; set; }

        public IClientSessionHandle Session { get; set; }

        public HistoricosRepository(
            ILogsRepository<HistoricosRepository<T>> logger,
            IOptions<MongoOpciones> configuration)
        {
            _logger = logger;
            _mongoClient = new MongoClient(configuration.Value.Connection);
        }

        public async Task<bool> GuardarHistorico(HistoricoDto<T> dto)
        {
            try
            {
                IMongoDatabase db = _mongoClient.GetDatabase($"Historico_{dto.modulo}");
                IMongoCollection<T> dbCollection = db.GetCollection<T>(dto.tabla);
                string jsonString = JsonConvert.SerializeObject(dto.referencia);
                JObject json = JsonConvert.DeserializeObject<dynamic>(jsonString);
                dynamic obj = json.ToObject<T>();
                await dbCollection.InsertOneAsync(obj);
                _logger.Informativo($"Histórico almacenado");
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al guardar el historico {dto.modulo} => {dto.tabla}, {ex.Message}");
                return false;
            }
        }

        public MongoClient ObtenerClienteMongo()
        {
            return _mongoClient;
        }
    }
}
