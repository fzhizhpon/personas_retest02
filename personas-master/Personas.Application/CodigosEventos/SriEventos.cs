namespace Personas.Application.CodigosEventos
{
    public static class SriEventos
    {
        // ? ok

        // * OBTENER
        public const string OBTENER_CONSOLIDAD_CONTRIBUYENTE = "0003-21-01";
        public const string CONSOLIDAD_CONTRIBUYENTE_NO_OBTENIDO = "0003-21-02";
        public const string OBTENER_INFORMACION_PLACA = "0003-21-03";
        public const string INFORMACION_PLACA_NO_OBTENIDO = "0003-21-04";

        // ! Error
        public const string OBTENER_CONSOLIDAD_CONTRIBUYENTE_ERROR = "-0003-21-01";
        public const string OBTENER_INFORMACION_PLACA_ERROR = "-0003-21-02";
    }
}