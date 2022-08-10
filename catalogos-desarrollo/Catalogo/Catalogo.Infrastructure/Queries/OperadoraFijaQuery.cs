namespace Catalogo.Infrastructure.Queries
{
    public static class OperadoraFijaQuery
    {
        public static string SelectOperadorasFijas(string esquema)
        {
            return "SELECT" +
                "   ofi.CODIGO_OPERADORA codigo," +
                "   ofi.NOMBRE descripcion" +
                " FROM VIMACOOP.SIFV_OPERADORAS_FIJO ofi" +
                " WHERE ofi.ESTADO = '1'";
        }
    }
}
