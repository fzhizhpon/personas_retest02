namespace Catalogo.Infrastructure.Queries
{
    public static class ProvinciaQuery
    {
        public static string SelectProvinciasPorPais(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PROVINCIA codigo," +
                "   p.DESCRIPCION" +
                $" FROM {esquema}.SIFV_PROVINCIAS p" +
                " WHERE p.CODIGO_PAIS = :codigoPais" +
                " AND p.ESTADO = '1'";
        }

        public static string SelectProvincia(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PAIS codigoPais," +
                "   p.CODIGO_PROVINCIA codigoProvincia," +
                "   p.DESCRIPCION," +
                "   p.PREFIJO_NUM_TELEFONO prefijoNumTelefono," +
                "   p.ESTADO," +
                "   p.CODIGO_ALTERNO codigoAlerno" +
                $" FROM {esquema}.SIFV_PROVINCIAS p" +
                " WHERE p.CODIGO_PAIS = :codigoPais" +
                " AND p.CODIGO_PROVINCIA = :codigoProvincia";
        }
    }
}
