namespace Catalogo.Infrastructure.Queries
{
    public class TipoContribuyenteQuery
    {
        public static string SelectTiposContribuyentes(string esquema)
        {
            return "SELECT" +
                "   tc.CODIGO," +
                "   tc.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_CONTRIBUYENTES tc" +
                " ORDER BY tc.ORDEN";
        }
    }
}
