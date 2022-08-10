using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Producto;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class ProductosService : IProductosService
    {
        private readonly IProductosRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public ProductosService(IProductosRepository repository,
            IMensajeRespuestaRepository mensajeRepository)
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerProductosActividadFinanciera(ObtenerProductoActividadFinancieraDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboDto> productos) =
                await _repository.ObtenerProductosActividadFinanciera(dto);
            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (productos is null)
                {
                    codigoRespuesta = ERespuesta.OK;
                    codigoEvento = -1; // No se encontraron datos
                }
            }
            else
            {
                codigoRespuesta = ERespuesta.ERROR;
                codigoEvento = -2; //Error al obtener los datos
            }

            //Llama al servicio de idioma
            string textoInfo = await _mensajeRepository.ObtenerTextoInfo(
                "Idioma", codigoEvento, "Modulo");

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                resultado = productos,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> ObtenerProductosCodigoGrupo(ObtenerProductoCodigoGrupoDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;
            (int codigo, IEnumerable<ComboDto> productosGrupo) =
                await _repository.ObtenerProductosCodigoGrupo(dto);
            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (productosGrupo is null)
                {
                    codigoRespuesta = ERespuesta.OK;
                    codigoEvento = -1; // No se encontraron datos
                }
            }
            else
            {
                codigoRespuesta = ERespuesta.ERROR;
                codigoEvento = -2; //Error al obtener los datos
            }
            
            //Llama al servicio de idioma
            string textoInfo = await _mensajeRepository.ObtenerTextoInfo(
                "Idioma", codigoEvento, "Modulo");

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                resultado = productosGrupo,
                mensajeUsuario = textoInfo
            };
            
        }
    }
}