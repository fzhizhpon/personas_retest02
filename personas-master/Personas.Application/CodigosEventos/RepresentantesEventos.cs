namespace Personas.Application.CodigosEventos
{
    public static class RepresentantesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_REPRESENTANTE = "0003-17-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_REPRESENTANTE = "0003-17-02";

        // * ELIMINAR 
        public const string ELIMINAR_REPRESENTANTE = "0003-17-03";

        // * OBTENER
        public const string OBTENER_REPRESENTANTES = "0003-17-04";
        public const string OBTENER_REPRESENTANTE = "0003-17-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string REPRESENTANTE_NO_GUARDADO = "-0003-17-01";
        public const string REPRESENTANTE_NO_ACTUALIZADO = "-0003-17-02";
        public const string REPRESENTANTE_NO_ELIMINADO = "-0003-17-03";
        public const string REPRESENTANTES_NO_OBTENIDOS = "-0003-17-04";
        public const string REPRESENTANTE_NO_OBTENIDOS = "-0003-17-05";
        public const string GUARDAR_REPRESENTANTE_ERROR_AUTO_REPRESENTACION = "-0003-17-06";

        // * NIVEL DEL REPOSITORIO

        public const string OBTENER_REPRESENTANTES_ERROR = "-0003-17-07";
        public const string OBTENER_REPRESENTANTES_PRINCIPALES_ERROR = "-0003-17-08";
        public const string OBTENER_REPRESENTANTE_ERROR = "-0003-17-09";
        public const string GUARDAR_REPRESENTANTE_ERROR = "-0003-17-10";
        public const string ACTUALIZAR_REPRESENTANTE_ERROR = "-0003-17-11";
        public const string ACTUALIZAR_REPRESENTANTE_PRINCIPALES_ERROR = "-0003-17-12";
        public const string ELIMINAR_REPRESENTANTE_ERROR = "-0003-17-13";

        public const string REPRESENTANTE_EXISTE = "-0003-17-14";

        // * MONGO
        public const string REPRESENTANTE_ERROR_FK = "-0003-17-15";
    }
}