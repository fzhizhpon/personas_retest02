namespace Personas.Application.EnumsEventos
{
    public static class DireccionesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_DIRECCION = "0003-06-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_DIRECCION = "0003-06-02";

        // * ELIMINAR 
        public const string ELIMINAR_DIRECCION = "0003-06-03";

        // * OBTENER
        public const string OBTENER_DIRECCIONES = "0003-06-04";
        public const string OBTENER_DIRECCION = "0003-06-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string DIRECCION_NO_GUARDADO = "-0003-06-01";
        public const string DIRECCION_NO_ACTUALIZADO = "-0003-06-02";
        public const string DIRECCION_NO_ELIMINADO = "-0003-06-03";
        public const string DIRECCIONES_NO_OBTENIDOS = "-0003-06-04";
        public const string DIRECCION_NO_OBTENIDOS = "-0003-06-05";


        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-06-06";
        public const string OBTENER_DIRECCIONES_ERROR = "-0003-06-07";
        public const string OBTENER_DIRECCION_ERROR = "-0003-06-08";
        public const string GUARDAR_DIRECCION_ERROR = "-0003-06-09";
        public const string ACTUALIZAR_DIRECCION_ERROR = "-0003-06-10";
        public const string ELIMINAR_DIRECCION_ERROR = "-0003-06-11";
        public const string DESMARCAR_DIRECCION_PRINCIPAL_ERROR = "-0003-06-12";
        public const string NUMERO_DIRECCIONES_PRINCIPÁLES = "-0003-06-13";


        // * MONGO
        public const string DIRECCION_ERROR_FK = "-0003-06-14";
        public const string DIRECCION_ERROR_CHECK_CONSTRAINT = "-0003-06-15";
    }
}