namespace Personas.Application.CodigosEventos
{
    public static class PersonasNaturalesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_PERSONA_NATURAL = "0003-11-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_PERSONA_NATURAL = "0003-11-02";

        // * OBTENER
        public const string OBTENER_INFORMACION_PERSONA = "0003-11-03";
        public const string OBTENER_PERSONA_NATURAL = "0003-11-04";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string PERSONA_NATURAL_NO_GUARDADO = "-0003-11-01";
        public const string PERSONA_NATURAL_NO_ACTUALIZADO = "-0003-11-02";
        public const string INFORMACION_PERSONA_NO_OBTENIDO = "-0003-11-03";
        public const string PERSONAS_NATURALES_NO_OBTENIDOS = "-0003-11-04";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_INFORMACION_PERSONA_ERROR = "-0003-11-05";
        public const string OBTENER_PERSONA_NATURAL_ERROR = "-0003-11-06";
        public const string GUARDAR_PERSONA_NATURAL_ERROR = "-0003-11-07";
        public const string ACTUALIZAR_PERSONA_NATURAL_ERROR = "-0003-11-08";
        public const string ACTUALIZAR_CONYUGE_PERSONA_NATURAL_ERROR = "-0003-11-09";

        // * MONGO
        public const string PERSONA_NATURAL_ERROR_UNIQUE_CONSTRAINT = "-0003-11-10";
        public const string PERSONA_NATURAL_ERROR_FK = "-0003-11-11";
        public const string PERSONA_NATURAL_ERROR_CHECK_CONSTRAINT = "-0003-11-12";
    }
}