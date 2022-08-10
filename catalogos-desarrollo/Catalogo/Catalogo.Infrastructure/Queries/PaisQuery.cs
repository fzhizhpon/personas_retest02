namespace Catalogo.Infrastructure.Queries
{
    public static class PaisQuery
    {
        public static string SelectPaises(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PAIS codigo," +
                "   p.DESCRIPCION" +
                $" FROM {esquema}.SIFV_PAISES p" +
                " WHERE ESTADO = '1'";
        }

        public static string SelectPaisesPaginacion(string esquema)
        {
            return "SELECT" +
                   "   p.CODIGO_PAIS codigo," +
                   "   p.DESCRIPCION" +
                   $" FROM {esquema}.SIFV_PAISES p" +
                   " WHERE ESTADO = '1' ORDER BY p.DESCRIPCION ASC";
        }
    }
}
