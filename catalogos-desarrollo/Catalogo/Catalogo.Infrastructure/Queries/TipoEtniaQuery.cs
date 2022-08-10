namespace Catalogo.Infrastructure.Queries
{
    public static class TipoEtniaQuery
    {
        public static string SelectTiposEtnias(string esquema)
        {
            return "SELECT" +
                "   te.CODIGO_TIPO_ETNIA codigo," +
                "   te.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_ETNIAS te" +
                " WHERE ESTADO = '1'";
        }
    }
}
