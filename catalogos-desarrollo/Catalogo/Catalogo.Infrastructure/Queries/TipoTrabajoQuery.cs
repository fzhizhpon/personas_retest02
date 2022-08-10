namespace Catalogo.Infrastructure.Queries
{
    public static class TipoTrabajoQuery
    {
        public static string SelectTiposTrabajos(string esquema)
        {
            return "SELECT" +
                "   tt.CODIGO_CATEGORIA codigo," +
                "   tt.DESCRIPCION" +
                $" FROM {esquema}.SIFV_CATEGORIAS_TRABAJOS tt";
        }
    }
}
