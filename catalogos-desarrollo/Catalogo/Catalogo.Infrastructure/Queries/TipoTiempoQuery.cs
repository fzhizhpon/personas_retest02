namespace Catalogo.Infrastructure.Queries
{
    public static class TipoTiempoQuery
    {
        public static string SelectTiposTiempo(string esquema)
        {
            return "SELECT" +
                "   tt.CODIGO_TIPO_TIEMPO codigo," +
                "   tt.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_TIEMPO tt";
        }
    }
}
