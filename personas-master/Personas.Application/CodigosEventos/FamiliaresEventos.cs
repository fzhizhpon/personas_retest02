namespace Personas.Application.CodigosEventos
{
    public static class FamiliaresEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_FAMILIAR = "0003-08-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_FAMILIAR = "0003-08-02";

        // * ELIMINAR 
        public const string ELIMINAR_FAMILIAR = "0003-08-03";

        // * OBTENER
        public const string OBTENER_FAMILIARES = "0003-08-04";
        public const string OBTENER_FAMILIAR = "0003-08-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string FAMILIAR_NO_GUARDADO = "-0003-08-01";
        public const string FAMILIAR_NO_ACTUALIZADO = "-0003-08-02";
        public const string FAMILIAR_NO_ELIMINADO = "-0003-08-03";
        public const string FAMILIARES_NO_OBTENIDOS = "-0003-08-04";
        public const string FAMILIAR_NO_OBTENIDOS = "-0003-08-05";
        public const string CODIGO_PERSONA_IGUAL_FAMILIAR = "-0003-08-06";
        
        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_FAMILIARES_ERROR = "-0003-08-07";
        public const string OBTENER_FAMILIAR_ERROR = "-0003-08-08";
        public const string GUARDAR_FAMILIAR_ERROR = "-0003-08-09";
        public const string ACTUALIZAR_FAMILIAR_ERROR = "-0003-08-10";
        public const string ELIMINAR_FAMILIAR_ERROR = "-0003-08-11";
        
        // * MONGO
        public const string FAMILIARES_ERROR_FK = "-0003-08-12";
        public const string FAMILIARES_ERROR_CHECK_CONSTRAINT = "-0003-08-13";

    }
}
