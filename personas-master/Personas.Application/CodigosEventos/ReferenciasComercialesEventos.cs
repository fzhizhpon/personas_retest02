namespace Personas.Application.CodigosEventos
{
    public static class ReferenciasComercialesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_REFERENCIA_COMERCIAL = "0003-13-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_REFERENCIA_COMERCIAL = "0003-13-02";

        // * ELIMINAR 
        public const string ELIMINAR_REFERENCIA_COMERCIAL = "0003-13-03";

        // * OBTENER
        public const string OBTENER_REFERENCIAS_COMERCIALES = "0003-13-04";
        public const string OBTENER_REFERENCIA_COMERCIAL = "0003-13-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string REFERENCIA_COMERCIAL_NO_GUARDADO = "-0003-13-01";
        public const string REFERENCIA_COMERCIAL_NO_ACTUALIZADO = "-0003-13-02";
        public const string REFERENCIA_COMERCIAL_NO_ELIMINADO = "-0003-13-03";
        public const string REFERENCIAS_COMERCIALES_NO_OBTENIDOS = "-0003-13-04";
        public const string REFERENCIA_COMERCIAL_NO_OBTENIDOS = "-0003-13-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-13-06";
        public const string OBTENER_REFERENCIAS_COMERCIALES_ERROR = "-0003-13-07";
        public const string OBTENER_REFERENCIA_COMERCIAL_ERROR = "-0003-13-08";
        public const string GUARDAR_REFERENCIA_COMERCIAL_ERROR = "-0003-13-09";
        public const string ACTUALIZAR_REFERENCIA_COMERCIAL_ERROR = "-0003-13-10";
        public const string ELIMINAR_REFERENCIA_COMERCIAL_ERROR = "-0003-13-11";

        // * MONGO
        public const string REFERENCIA_COMERCIAL_ERROR_FK = "-0003-13-12";
        public const string  REFERENCIA_COMERCIAL_ERROR_CHECK_CONSTRAINT = "-0003-13-13";
    }
}