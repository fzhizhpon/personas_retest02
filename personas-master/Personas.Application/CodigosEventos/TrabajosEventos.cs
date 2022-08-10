namespace Personas.Application.CodigosEventos
{
    public static class TrabajosEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_TRABAJO = "0003-20-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_TRABAJO = "0003-20-02";

        // * ELIMINAR 
        public const string ELIMINAR_TRABAJO = "0003-20-03";

        // * OBTENER
        public const string OBTENER_TRABAJOS = "0003-20-04";
        public const string OBTENER_TRABAJO = "0003-20-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string TRABAJO_NO_GUARDADO = "-0003-20-01";
        public const string TRABAJO_NO_ACTUALIZADO = "-0003-20-02";
        public const string TRABAJO_NO_ELIMINADO = "-0003-20-03";
        public const string TRABAJOS_NO_OBTENIDOS = "-0003-20-04";
        public const string TRABAJO_NO_OBTENIDOS = "-0003-20-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-20-06";
        public const string OBTENER_TRABAJOS_ERROR = "-0003-20-07";
        public const string OBTENER_TRABAJO_ERROR = "-0003-20-08";
        public const string GUARDAR_TRABAJO_ERROR = "-0003-20-09";
        public const string ACTUALIZAR_TRABAJO_ERROR = "-0003-20-10";
        public const string ELIMINAR_TRABAJO_ERROR = "-0003-20-11";

        public const string TRABAJO_EXISTE = "-0003-20-12";

        // * MONGO
        public const string TRABAJO_ERROR_FK = "-0003-20-13";
    }
}