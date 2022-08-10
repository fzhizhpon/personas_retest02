namespace Catalogo.Infrastructure.Queries
{
    public static class SucursalQuery
    {
        public static string SelectSucursales(string esquema)
        {
            return "SELECT" +
                "   s.CODIGO_SUCURSAL codigo," +
                "   s.DESCRIPCION" +
                $" FROM {esquema}.SIFV_SUCURSALES s" +
                " WHERE s.ESTADO = '1'";
        }
    }
}
