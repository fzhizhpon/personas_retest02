namespace Personas.Application.CodigosEventos
{
    public static class CorreosElectronicosEventos
	{
        // ? ok

        // * GUARDAR
        public const string GUARDAR_CORREO = "0003-05-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_CORREO = "0003-05-02";

        // * ELIMINAR 
        public const string ELIMINAR_CORREO = "0003-05-03";

        // * OBTENER
        public const string OBTENER_CORREOS = "0003-05-04";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string CORREO_NO_GUARDADO = "-0003-05-01";
        public const string CORREO_NO_ACTUALIZADO = "-0003-05-02";
        public const string CORREO_NO_ELIMINADO = "-0003-05-03";
        public const string CORREOS_NO_OBTENIDOS = "-0003-05-04";
        public const string CORREO_EXISTE = "-0003-05-05";
        public const string CORREO_NO_OBTENIDO = "-0003-05-06";
     
        
        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-05-07";
        public const string OBTENER_CORREOS_ERROR = "-0003-05-08";
        public const string OBTENER_CORREO_ERROR = "-0003-05-09";
        public const string GUARDAR_CORREO_ERROR = "-0003-05-10";
        public const string ACTUALIZAR_CORREO_ERROR = "-0003-05-11";
        public const string ELIMINAR_CORREO_ERROR = "-0003-05-12";
        public const string DESMARCAR_CORRE_PRINCIPAL_ERROR = "-0003-05-13";
        public const string NUMERO_CORREOS_PRINCIPÁLES = "-0003-05-14";
        
        
        // * MONGO
        public const string CORREO_ELECTRONICO_ERROR_FK = "-0003-05-15";
        public const string CORREO_ELECTRONICO_ERROR_CHECK_CONSTRAINT = "-0003-05-16";
        
        
    }
}

