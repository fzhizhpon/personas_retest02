namespace Personas.Application.CodigosEventos
{
    public static class EstadosFinancierosEventos
    {
        // ? ok

        // * GUARDAR
        public const string GUARDAR_ESTADO_FINANCIERO = "0003-07-01";

        // * ACTUALIZAR 
        public const string ACTUALIZAR_ESTADO_FINANCIERO = "0003-07-02";

        // * OBTENER
        public const string OBTENER_ESTADOS_FINANCIEROS = "0003-07-03";

        // ! ERORRES

        // * NIVEL DEL SERVICIO
        public const string ESTADO_FINANCIERO_NO_GUARDADO = "-0003-07-01";
        public const string ESTADO_FINANCIERO_NO_ACTUALIZADO = "-0003-07-02";
        public const string ESTADOS_FINANCIEROS_NO_OBTENIDOS = "-0003-07-03";

        // * NIVEL DEL REPOSITORIO
        public const string OBTENER_ESTADOS_FINANCIEROS_ERROR = "-0003-07-04";
        public const string GUARDAR_ESTADO_FINANCIERO_ERROR = "-0003-07-05";
        public const string ACTUALIZAR_ESTADO_FINANCIERO_ERROR = "-0003-07-06";
        public const string OBTENER_VALOR_CUENTA_ERROR = "-0003-07-07";

        // * MONGO
        public const string ESTADO_FINANCIERO_ERROR_FK = "-0003-07-08";
    }
}