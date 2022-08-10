namespace Personas.Application.CodigosEventos
{
    public static class TelefonosFijosEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_TELEFONO_FIJO = "0003-18-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_TELEFONO_FIJO = "0003-18-02";

        // * ELIMINAR 
        public const string ELIMINAR_TELEFONO_FIJO = "0003-18-03";

        // * OBTENER
        public const string OBTENER_TELEFONOS_FIJOS = "0003-18-04";
        public const string OBTENER_TELEFONO_FIJO = "0003-18-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string TELEFONOS_FIJO_NO_GUARDADO = "-0003-18-01";
        public const string TELEFONOS_FIJO_NO_ACTUALIZADO = "-0003-18-02";
        public const string TELEFONOS_FIJO_NO_ELIMINADO = "-0003-18-03";
        public const string TELEFONOS_FIJOS_NO_OBTENIDOS = "-0003-18-04";
        public const string TELEFONOS_FIJO_NO_OBTENIDOS = "-0003-18-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-18-06";
        public const string OBTENER_TELEFONOS_FIJOS_ERROR = "-0003-18-07";
        public const string OBTENER_TELEFONO_FIJO_ERROR = "-0003-18-08";
        public const string GUARDAR_TELEFONO_FIJO_ERROR = "-0003-18-09";
        public const string ACTUALIZAR_TELEFONO_FIJO_ERROR = "-0003-18-10";
        public const string ELIMINAR_TELEFONO_FIJO_ERROR = "-0003-18-11";


        public const string TELEFONO_FIJO_EXISTE = "-0003-18-12";

        // * MONGO
        public const string TELEFONO_FIJO_ERROR_FK = "-0003-18-13";
    }
}