namespace Personas.Application.CodigosEventos
{
    public static class ReferenciasFinancierasEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_REFERENCIA_FINANCIERA = "0003-14-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_REFERENCIA_FINANCIERA = "0003-14-02";

        // * ELIMINAR 
        public const string ELIMINAR_REFERENCIA_FINANCIERA = "0003-14-03";

        // * OBTENER
        public const string OBTENER_REFERENCIAS_FINANCIERAS = "0003-14-04";
        public const string OBTENER_REFERENCIA_FINANCIERA = "0003-14-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string REFERENCIA_FINANCIERA_NO_GUARDADO = "-0003-14-01";
        public const string REFERENCIA_FINANCIERA_NO_ACTUALIZADO = "-0003-14-02";
        public const string REFERENCIA_FINANCIERA_NO_ELIMINADO = "-0003-14-03";
        public const string REFERENCIAS_FINANCIERAS_NO_OBTENIDOS = "-0003-14-04";
        public const string REFERENCIA_FINANCIERA_NO_OBTENIDOS = "-0003-14-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-14-06";
        public const string OBTENER_REFERENCIAS_FINANCIERAS_ERROR = "-0003-14-07";
        public const string OBTENER_REFERENCIA_FINANCIERA_ERROR = "-0003-14-08";
        public const string GUARDAR_REFERENCIA_FINANCIERA_ERROR = "-0003-14-09";
        public const string ACTUALIZAR_REFERENCIA_FINANCIERA_ERROR = "-0003-14-10";
        public const string ELIMINAR_REFERENCIA_FINANCIERA_ERROR = "-0003-14-11";
        
        // * MONGO
        public const string REFERENCIA_FINANCIERA_ERROR_FK = "-0003-14-12";
        
        
    }
}