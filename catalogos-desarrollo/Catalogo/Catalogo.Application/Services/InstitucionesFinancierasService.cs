using Catalogo.Core.DTOs;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class InstitucionesFinancierasService : IInstitucionesFinancierasService
    {
        private readonly IInstitucionesFinancierasRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public InstitucionesFinancierasService(IInstitucionesFinancierasRepository repository,
            IMensajeRespuestaRepository mensajeRepository)
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerInstitucionesFinancieras(ObtenerInstitucionFinancieraDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboDto> institucionesFinancieras) =
                await _repository.ObtenerInstitucionesFinancieras(dto);
            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (institucionesFinancieras is null)
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
                resultado = institucionesFinancieras,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> ObtenerInsitucionesFinancierasFull()
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;
            (int codigo, IEnumerable<ComboDto> institucionesFinancierasFull) =
                await _repository.ObtenerInsitucionesFinancierasFull();
            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (institucionesFinancierasFull is null)
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
                resultado = institucionesFinancierasFull,
                mensajeUsuario = textoInfo
            };
            
        }
    }
}