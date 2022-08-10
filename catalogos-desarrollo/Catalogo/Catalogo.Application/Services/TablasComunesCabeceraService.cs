using Catalogo.Core.DTOs;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class TablasComunesCabeceraService : ITablasComunesCabeceraService
    {
        private readonly ITablasComunesCabeceraRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public TablasComunesCabeceraService(ITablasComunesCabeceraRepository repository,
            IMensajeRespuestaRepository mensajeRepository)
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerTablasComunesCabeceras(ObtenerTablasComunesCabeceraDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboDto> tablasComunesCabecera) =
                await _repository.ObtenerTablasComunesCabeceras(dto);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (tablasComunesCabecera is null)
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
                resultado = tablasComunesCabecera,
                mensajeUsuario = textoInfo
            };
        }
    }
}