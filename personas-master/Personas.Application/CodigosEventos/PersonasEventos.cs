namespace Personas.Application.CodigosEventos
{
    public static class PersonasEventos
    {
        // ? ok

        // * ACTUALIZAR 
        public const string ACTUALIZAR_PERSONA = "0003-10-01";
        
        // * OBTENER
        public const string OBTENER_PERSONAS = "0003-10-02";
        public const string OBTENER_PERSONA = "0003-10-03";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string PERSONA_NO_ACTUALIZADO = "-0003-10-01";
        public const string PERSONAS_NO_OBTENIDOS = "-0003-10-02";
        public const string PERSONA_NO_OBTENIDOS = "-0003-10-03";
        public const string ERROR_CEDULA_INVALIDA = "-0003-10-04";
        public const string ERROR_RUC_INVALIDO = "-0003-10-05";
        public const string ERROR_PERSONA_YA_EXISTE = "-0003-10-13";

        
        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-10-06";
        public const string OBTENER_PERSONAS_ERROR = "-0003-10-07";
        public const string OBTENER_PERSONA_ERROR = "-0003-10-08";
        public const string GUARDAR_PERSONA_ERROR = "-0003-10-09";
        public const string ACTUALIZAR_PERSONA_ERROR = "-0003-10-10";
        public const string COLOCAR_FECHA_ULT_ACTUALIZACION_ERROR = "-0003-10-11";
        public const string OBTENER_PERSONA_POR_ID_ERROR = "-0003-10-12";
        
        // * MONGO
        public const string PERSONA_ERROR_UNIQUE_CONSTRAINT = "-0003-10-14";
        public const string PERSONA_ERROR_FK = "-0003-10-15";

    }
}
