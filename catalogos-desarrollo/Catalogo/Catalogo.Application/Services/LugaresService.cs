using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Lugar;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class LugaresService : ILugaresService
    {
        private readonly ILugaresRepository _lugaresRepository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public LugaresService(ILugaresRepository lugaresRepository, IMensajeRespuestaRepository mensajeRepository)
        {
            _lugaresRepository = lugaresRepository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerLugares(ObtenerLugaresDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<LugarOutDto> lugares) = await _lugaresRepository.SelectLugares(dto);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (lugares is null)
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
                resultado = lugares,
                mensajeUsuario = textoInfo
            };
        }
    }
}
