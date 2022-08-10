namespace Personas.Application.CodigosEventos
{
    public class TelefonosMovilesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_TELEFONO_MOVIL = "0003-19-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_TELEFONO_MOVIL = "0003-19-02";

        // * ELIMINAR 
        public const string ELIMINAR_TELEFONO_MOVIL = "0003-19-03";

        // * OBTENER
        public const string OBTENER_TELEFONOS_MOVILES = "0003-19-04";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string TELEFONO_MOVIL_NO_GUARDADO = "-0003-19-01";
        public const string TELEFONO_MOVIL_NO_ACTUALIZADO = "-0003-19-02";
        public const string TELEFONO_MOVIL_NO_ELIMINADO = "-0003-19-03";
        public const string TELEFONOS_MOVILES_NO_OBTENIDOS = "-0003-19-04";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_TELEFONOS_MOVILES_ERROR = "-0003-19-05";
        public const string GUARDAR_TELEFONO_MOVIL_ERROR = "-0003-19-06";
        public const string ACTUALIZAR_TELEFONO_MOVIL_ERROR = "-0003-19-07";
        public const string ELIMINAR_TELEFONO_MOVIL_ERROR = "-0003-19-08";

        // * MONGO
        public const string TELEFONO_MOVIL_ERROR_FK = "-0003-19-09";
        public const string TELEFONO_MOVIL_ERROR_CHECK_CONSTRAINT = "-0003-19-10";
    }
}