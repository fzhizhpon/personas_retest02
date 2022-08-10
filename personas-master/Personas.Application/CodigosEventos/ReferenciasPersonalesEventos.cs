namespace Personas.Application.CodigosEventos
{
    public static class ReferenciasPersonalesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_REFERENCIA_PERSONAL = "0003-15-01";

        // * ELIMINAR 
        public const string ELIMINAR_REFERENCIA_PERSONAL = "0003-15-02";

        // * OBTENER
        public const string OBTENER_REFERENCIAS_PERSONALES = "0003-15-03";
        public const string OBTENER_REFERENCIA_PERSONAL = "0003-15-04";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string REFERENCIA_PERSONAL_NO_GUARDADO = "-0003-15-01";
        public const string REFERENCIA_PERSONAL_NO_ELIMINADO = "-0003-15-02";
        public const string REFERENCIAS_PERSONALES_NO_OBTENIDOS = "-0003-15-03";
        public const string REFERENCIA_PERSONAL_NO_OBTENIDOS = "-0003-15-04";


        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-15-05";
        public const string OBTENER_REFERENCIAS_PERSONALES_ERROR = "-0003-15-06";
        public const string OBTENER_REFERENCIA_PERSONAL_ERROR = "-0003-15-07";
        public const string GUARDAR_REFERENCIA_PERSONAL_ERROR = "-0003-15-08";
        public const string ELIMINAR_REFERENCIA_PERSONAL_ERROR = "-0003-15-09";

        // * MONGO
        public const string REFERENCIA_PERSONAL_ERROR_FK = "-0003-15-10";
        
    }
}