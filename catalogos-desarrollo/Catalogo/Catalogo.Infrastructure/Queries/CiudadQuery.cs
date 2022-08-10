namespace Catalogo.Infrastructure.Queries
{
    public static class CiudadQuery
    {
        public static string SelectCiudadPorProvincia(string esquema)
        {
            return "SELECT" +
                "   c.CODIGO_CIUDAD codigo," +
                "   c.DESCRIPCION" +
                " FROM VIMACOOP.SIFV_CIUDADES c" +
                " WHERE c.CODIGO_PAIS = :codigoPais" +
                " AND c.CODIGO_PROVINCIA = :codigoProvincia" +
                " AND c.ESTADO = '1'";
        }
    }
}
