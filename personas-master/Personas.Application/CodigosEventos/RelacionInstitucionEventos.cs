namespace Personas.Application.CodigosEventos
{
    public class RelacionInstitucionEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_RELACION_INSTITUCIONAL = "0003-16-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_RELACION_INSTITUCIONAL = "0003-16-02";

        // * OBTENER
        public const string OBTENER_RELACIONES_INSTITUCIONALES = "0003-16-03";
        public const string OBTENER_RELACION_INSTITUCIONAL = "0003-16-04";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string RELACION_INSTITUCIONAL_NO_GUARDADO = "-0003-16-01";
        public const string RELACION_INSTITUCIONAL_NO_ACTUALIZADO = "-0003-16-02";
        public const string RELACIONES_INSTITUCIONALES_NO_OBTENIDOS = "-0003-16-03";
        public const string RELACION_INSTITUCIONAL_NO_OBTENIDOS = "-0003-16-04";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_RELACIONES_INSTITUCIONALES_ERROR = "-0003-16-05";
        public const string OBTENER_RELACION_INSTITUCIONAL_ERROR = "-0003-16-06";
        public const string GUARDAR_RELACION_INSTITUCIONAL_ERROR = "-0003-16-07";
        public const string ACTUALIZAR_RELACION_INSTITUCIONAL_ERROR = "-0003-16-08";
        
        // * MONGO
        public const string RELACION_INSTITUCIONAL_ERROR_FK = "-0003-16-09";
    }
}