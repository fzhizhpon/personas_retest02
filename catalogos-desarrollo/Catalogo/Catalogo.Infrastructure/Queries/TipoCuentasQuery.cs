namespace Catalogo.Infrastructure.Queries
{
    public static class TipoCuentaQuery
    {
        public static string SelectTipoCuenta(string esquema)
        {
            return "SELECT" +
                "   tc.TIPO_CUENTA codigo," +
                "   tc.DESCRIPCION descripcion" +
                $" FROM {esquema}.CAPTA_TIPOS_CUENTAS tc" +
                " WHERE tc.ESTADO = '1'";
        }
    }
}
