namespace Personas.Application.CodigosEventos
{
    public class BienesMueblesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_BIEN_MUEBLE = "0003-04-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_BIEN_MUEBLE = "0003-04-02";

        // * ELIMINAR 
        public const string ELIMINAR_BIEN_MUEBLE = "0003-04-03";

        // * OBTENER
        public const string OBTENER_BIENES_MUEBLES = "0003-04-04";
        public const string OBTENER_BIEN_MUEBLE = "0003-04-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string BIEN_MUEBLE_NO_GUARDADO = "-0003-04-01";
        public const string BIEN_MUEBLE_NO_ACTUALIZADO = "-0003-04-02";
        public const string BIEN_MUEBLE_NO_ELIMINADO = "-0003-04-03";
        public const string BIENES_MUEBLES_NO_OBTENIDOS = "-0003-04-04";
        public const string BIEN_MUEBLE_NO_OBTENIDOS = "-0003-04-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-04-06";
        public const string OBTENER_BIENES_MUEBLES_ERROR = "-0003-04-07";
        public const string OBTENER_BIEN_MUEBLE_ERROR = "-0003-04-08";
        public const string GUARDAR_BIEN_MUEBLE_ERROR = "-0003-04-09";
        public const string ACTUALIZAR_BIEN_MUEBLE_ERROR = "-0003-04-10";
        public const string ELIMINAR_BIEN_MUEBLE_ERROR = "-0003-04-11";

        // * mongo
        public const string BIEN_MUEBLE_ERROR_FK = "-0003-04-12";
    }
}