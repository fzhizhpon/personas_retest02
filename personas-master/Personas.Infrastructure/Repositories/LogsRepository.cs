using Microsoft.Extensions.Logging;
using Personas.Core.App;
using Personas.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Repositories
{

    public class LogsRepository<T> : ILogsRepository<T>
    {
        private readonly ILogger<T> _logger;
        private readonly ConfiguracionApp _config;

        public LogsRepository(ILogger<T> logger, ConfiguracionApp config)
        {
            _logger = logger;
            _config = config;
        }

        public void Error(string mensaje)
        {
            _logger.LogError($"MS: {_config.Modulo} -- GUID: {_config.guid} -- MENSAJE: {mensaje}");
        }

        public void Informativo(string mensaje)
        {
            _logger.LogInformation($"MS: {_config.Modulo} -- GUID: {_config.guid} -- MENSAJE: {mensaje}");
        }

        public void Warning(string mensaje)
        {
            _logger.LogWarning($"MS: {_config.Modulo} --- GUID: {_config.guid} -- MENSAJE: {mensaje}");
        }
    }
}
