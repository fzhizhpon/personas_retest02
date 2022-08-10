using Catalogo.Core.DTOs;
using Catalogo.Core.DTOs.Profesion;
using Catalogo.Core.Entities;
using Catalogo.Core.Enums;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class ProfesionService : IProfesionService
    {
        private readonly IProfesionRepository _repository;
        private readonly IMensajeRespuestaRepository _mensajeRepository;

        public ProfesionService(IProfesionRepository repository, IMensajeRespuestaRepository mensajeRepository)
        {
            _repository = repository;
            _mensajeRepository = mensajeRepository;

        }

        public async Task<Respuesta> ActualizarProfesion(ActualizarProfesionDto profesion)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, int resultado) = await _repository.UpdateProfesion(profesion);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (resultado == 0)
                {
                    codigoRespuesta = ERespuesta.ERROR;
                    codigoEvento = -1; // No se actualizo los datos
                }
            }
            else
            {
                codigoRespuesta = ERespuesta.ERROR;
                codigoEvento = -2; //Error al actualizar los datos
            }

            //Llama al servicio de idioma
            string textoInfo = await _mensajeRepository.ObtenerTextoInfo(
              "Idioma", codigoEvento, "Modulo");

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                resultado = null,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> EliminarProfesion(int codigoProfesion)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, int resultado) = await _repository.DeleteProfesion(codigoProfesion);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (resultado == 0)
                {
                    codigoRespuesta = ERespuesta.ERROR;
                    codigoEvento = -1; // No se elimino los datos
                }
            }
            else
            {
                codigoRespuesta = ERespuesta.ERROR;
                codigoEvento = -2; //Error al eliminar los datos
            }

            //Llama al servicio de idioma
            string textoInfo = await _mensajeRepository.ObtenerTextoInfo(
              "Idioma", codigoEvento, "Modulo");

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                resultado = null,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> GuardarProfesion(Profesion profesion)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, int resultado) = await _repository.InsertProfesion(profesion);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (resultado == 0)
                {
                    codigoRespuesta = ERespuesta.ERROR;
                    codigoEvento = -1; // No se guardo los datos
                }
            }
            else
            {
                codigoRespuesta = ERespuesta.ERROR;
                codigoEvento = -2; //Error al guardar los datos
            }

            //Llama al servicio de idioma
            string textoInfo = await _mensajeRepository.ObtenerTextoInfo(
              "Idioma", codigoEvento, "Modulo");

            return new Respuesta()
            {
                codigo = codigoRespuesta,
                resultado = null,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> ObtenerProfesion(int codigoProfesion)
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, Profesion profesion) = await _repository.SelectProfesion(codigoProfesion);

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (profesion is null)
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
                resultado = profesion,
                mensajeUsuario = textoInfo
            };
        }

        public async Task<Respuesta> ObtenerProfesiones()
        {
            ERespuesta codigoRespuesta = ERespuesta.OK;
            int codigoEvento = 0;

            (int codigo, IEnumerable<ComboDto> profesiones) = await _repository.SelectProfesiones();

            if (codigo == CodigosLogicaInterna.CODIGO_GENERICO_OK_INTERNO)
            {
                if (profesiones is null)
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
                resultado = profesiones,
                mensajeUsuario = textoInfo
            };
        }
    }
}
