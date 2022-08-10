using Catalogo.Core.DTOs;
using Catalogo.Core.Interfaces.IRepositories;
using CoopCrea.Cross.Cache.Src;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class PersistenciaRepository : IPersistenciaRepository
    {
        private readonly ILogger _logger;
        private readonly IExtensionCache _extensionCache;

        public PersistenciaRepository(IExtensionCache extensionCache,
            ILogger<PersistenciaRepository> logger)
        {
            _logger = logger;
            _extensionCache = extensionCache;
        }

        public int EliminarCatalogos(string key)
        {
            try
            {
                _extensionCache.DeleteData(key); // Se agrega a redis
                return CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: No se pudo eliminar persistencia de catalogo {key} => , {ex}");
                return CodigosLogicaInterna.CODIGO_GENERICO_ERROR_INTERNO;
            }
        }
    }
}
