namespace Personas.Application.CodigosEventos
{
    public class InformacionAdicionalEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_INFORMACION_ADICIONAL = "0003-09-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_INFORMACION_ADICIONAL = "0003-09-02";

        // * OBTENER
        public const string OBTENER_INFORMACION_ADICIONAL = "0003-09-03";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string INFORMACION_ADICIONAL_NO_GUARDADO = "-0003-09-01";
        public const string INFORMACION_ADICIONAL_NO_ACTUALIZADO = "-0003-09-02";
        public const string INFORMACION_ADICIONAL_NO_OBTENIDOS = "-0003-09-03";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_INFORMACION_ADICIONAL_ERROR = "-0003-09-04";
        public const string GUARDAR_INFORMACION_ADICIONAL_ERROR = "-0003-09-05";
        public const string ACTUALIZAR_INFORMACION_ADICIONAL_ERROR = "-0003-09-06";

        // * MONGO
        public const string INFORMACION_ADICIONAL_ERROR_FK = "-0003-09-07";
    }
}