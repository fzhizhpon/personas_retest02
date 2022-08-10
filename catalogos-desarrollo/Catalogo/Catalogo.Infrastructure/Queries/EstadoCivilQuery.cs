namespace Catalogo.Infrastructure.Queries
{
    public static class EstadoCivilQuery
    {
        public static string SelectEstadosCiviles(string esquema)
        {
            return "SELECT" +
                "   tec.CODIGO_TIPO_ESTADO_CIVIL codigo," +
                "   tec.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_ESTADOS_CIVILES tec" +
                " WHERE tec.ESTADO = '1'";
        }
    }
}
