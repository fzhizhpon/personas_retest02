using Catalogo.Core.DTOs;
using Catalogo.Core.Entities;
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
    public class ActividadesEconomicasService : IActividadesEconomicasService
    {

        protected readonly IActividadesEconomicasRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public ActividadesEconomicasService(
            IActividadesEconomicasRepository repository,
            IMensajeRespuestaRepository mensajeRepository
        )
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;
        }

        public async Task<Respuesta> ObtenerActividadesEconomicas(ActividadComercialDto dto)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboStringDto> actividades) = await _repository.ObtenerActividadesEconomicas(dto);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (actividades is null)
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
                resultado = actividades,
                mensajeUsuario = textoInfo
            };
        }


    }
}
