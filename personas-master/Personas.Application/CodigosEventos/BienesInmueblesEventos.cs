namespace Personas.Application.CodigosEventos
{
    public class BienesInmueblesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_BIEN_INMUEBLE = "0003-02-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_BIEN_INMUEBLE = "0003-02-02";

        // * ELIMINAR 
        public const string ELIMINAR_BIEN_INMUEBLE = "0003-02-03";

        // * OBTENER
        public const string OBTENER_BIENES_INMUEBLES = "0003-02-04";
        public const string OBTENER_BIEN_INMUEBLE = "0003-02-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string BIEN_INMUEBLE_NO_GUARDADO = "-0003-02-01";
        public const string BIEN_INMUEBLE_NO_ACTUALIZADO = "-0003-02-02";
        public const string BIEN_INMUEBLE_NO_ELIMINADO = "-0003-02-03";
        public const string BIENES_INMUEBLES_NO_OBTENIDOS = "-0003-02-04";
        public const string BIEN_INMUEBLE_NO_OBTENIDOS = "-0003-02-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-02-06";
        public const string OBTENER_BIENES_INMUEBLES_ERROR = "-0003-02-07";
        public const string OBTENER_BIEN_INMUEBLE_ERROR = "-0003-02-08";
        public const string GUARDAR_BIEN_INMUEBLE_ERROR = "-0003-02-09";
        public const string ACTUALIZAR_BIEN_INMUEBLE_ERROR = "-0003-02-10";
        public const string ELIMINAR_BIEN_INMUEBLE_ERROR = "-0003-02-11";


        // * MONGO
        public const string BIEN_INMUEBLE_ERROR_FK = "-0003-02-12";
        public const string BIEN_INMUEBLE_ERROR_CHECK_CONSTRAINT = "-0003-02-13";
    }
}