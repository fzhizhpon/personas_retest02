namespace Personas.Application.CodigosEventos
{
    public class BienesIntangiblesEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_BIEN_INTANGIBLE = "0003-03-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_BIEN_INTANGIBLE = "0003-03-02";

        // * ELIMINAR 
        public const string ELIMINAR_BIEN_INTANGIBLE = "0003-03-03";

        // * OBTENER
        public const string OBTENER_BIENES_INMUEBLES = "0003-03-04";
        public const string OBTENER_BIEN_INTANGIBLE = "0003-03-05";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string BIEN_INTANGIBLE_NO_GUARDADO = "-0003-03-01";
        public const string BIEN_INTANGIBLE_NO_ACTUALIZADO = "-0003-03-02";
        public const string BIEN_INTANGIBLE_NO_ELIMINADO = "-0003-03-03";
        public const string BIENES_INTANGIBLES_NO_OBTENIDOS = "-0003-03-04";
        public const string BIEN_INTANGIBLE_NO_OBTENIDOS = "-0003-03-05";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_NUMERO_REGISTRO_MAX_ERROR = "-0003-03-06";
        public const string OBTENER_BIENES_INTANGIBLES_ERROR = "-0003-03-07";
        public const string OBTENER_BIEN_INTANGIBLE_ERROR = "-0003-03-08";
        public const string GUARDAR_BIEN_INTANGIBLE_ERROR = "-0003-03-09";
        public const string ACTUALIZAR_BIEN_INTANGIBLE_ERROR = "-0003-03-10";
        public const string ELIMINAR_BIEN_INTANGIBLE_ERROR = "-0003-03-11";

        // * mongo
        public const string BIEN_INTANGIBLE_ERROR_FK = "-0003-03-12";
    }
}