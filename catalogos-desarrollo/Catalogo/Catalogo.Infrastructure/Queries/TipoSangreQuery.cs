namespace Catalogo.Infrastructure.Queries
{
    public static class TipoSangreQuery
    {
        public static string SelectTiposSangre(string esquema)
        {
            return "SELECT" +
                "   sts.CODIGO_TIPO_SANGRE codigo," +
                "   sts.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_SANGRE sts" +
                " WHERE sts.ESTADO = '1'";
        }
    }
}
