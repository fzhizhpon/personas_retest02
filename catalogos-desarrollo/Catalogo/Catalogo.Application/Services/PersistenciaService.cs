using Catalogo.Core.DTOs;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class PersistenciaService : IPersistenciaService
    {

        protected readonly IPersistenciaRepository _repository;

        public PersistenciaService(IPersistenciaRepository repository)
        {
            _repository = repository;
        }

        public Respuesta EliminarCatalogos(EliminarPersistenciaDto dto)
        {
            ERespuesta result = ERespuesta.OK;
            try
            {
                _repository.EliminarCatalogos(dto.key);
            }
            catch (Exception)
            {
                result = ERespuesta.ERROR;
            }

            return new Respuesta()
            {
                resultado = null,
                mensajeUsuario = null,
                codigo = result
            };
        }
    }
}
