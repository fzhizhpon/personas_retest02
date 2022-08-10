namespace Catalogo.Infrastructure.Queries
{
    public static class TipoDiscapacidadQuery
    {
        public static string SelectTiposDiscapacidades(string esquema)
        {
            return "SELECT" +
                "   td.CODIGO_TIPO_DISCAPACIDAD codigo," +
                "   td.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_DISCAPACIDADES td" +
                " WHERE td.ESTADO = '1'";
        }
    }
}
