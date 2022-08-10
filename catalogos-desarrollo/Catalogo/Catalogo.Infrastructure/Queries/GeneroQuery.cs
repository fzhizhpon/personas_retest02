namespace Catalogo.Infrastructure.Queries
{
    public static class GeneroQuery
    {
        public static string SelectGeneros(string esquema)
        {
            return "SELECT" +
                "   g.CODIGO_GENERO codigo," +
                "   g.DESCRIPCION" +
                " FROM VIMACOOP.SIFV_GENEROS g" +
                " WHERE g.ESTADO = '1'";
        }
    }
}
