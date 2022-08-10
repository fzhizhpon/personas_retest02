namespace Personas.Application.CodigosEventos
{
    public static class PersonasNoNaturalesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_PERSONA_NO_NATURAL = "0003-12-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_PERSONA_NO_NATURAL = "0003-12-02";

        // * OBTENER
        public const string OBTENER_PERSONA_NO_NATURAL = "0003-12-03";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string PERSONA_NO_NATURAL_NO_GUARDADO = "-0003-12-01";
        public const string PERSONA_NO_NATURAL_NO_ACTUALIZADO = "-0003-12-02";
        public const string PERSONA_NO_NATURAL_NO_OBTENIDO = "-0003-12-03";
        
        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_PERSONA_NO_NATURAL_ERROR = "-0003-12-04";
        public const string GUARDAR_PERSONA_NO_NATURAL_ERROR = "-0003-12-05";
        public const string ACTUALIZAR_PERSONA_NO_NATURAL_ERROR = "-0003-12-06";
        
        // * MONGO
        public const string PERSONA_NO_NATURAL_ERROR_FK = "-0003-12-07";

    }
}
