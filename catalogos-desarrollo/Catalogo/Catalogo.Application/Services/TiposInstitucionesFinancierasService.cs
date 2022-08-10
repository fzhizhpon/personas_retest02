using Catalogo.Core.DTOs;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class TiposInstitucionesFinancierasService : ITiposInstitucionesFinancierasService
    {
        private readonly ITiposInstitucionesFinancierasRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public TiposInstitucionesFinancierasService(ITiposInstitucionesFinancierasRepository repository,
            IMensajeRespuestaRepository mensajeRepository)
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerTiposInstitucionesFinancieras()
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboDto> tiposInstitucionesFinancieras) =
                await _repository.ObtenerTiposInstitucionesFinancieras();

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (tiposInstitucionesFinancieras is null)
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
                resultado = tiposInstitucionesFinancieras,
                mensajeUsuario = textoInfo
            };
        }
    }
}