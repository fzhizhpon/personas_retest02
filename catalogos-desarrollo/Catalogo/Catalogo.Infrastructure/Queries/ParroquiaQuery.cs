namespace Catalogo.Infrastructure.Queries
{
    public static class ParroquiaQuery
    {
        public static string SelectParroquiasPorCiudad(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PARROQUIA codigo," +
                "   p.DESCRIPCION" +
                $" FROM {esquema}.SIFV_PARROQUIAS p" +
                " WHERE p.CODIGO_PAIS = :codigoPais" +
                " AND p.CODIGO_PROVINCIA = :codigoProvincia" +
                " AND p.CODIGO_CIUDAD = :codigoCiudad" +
                " AND p.ESTADO = '1'";
        }
    }
}
