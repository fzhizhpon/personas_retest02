namespace Catalogo.Infrastructure.Queries
{
    public static class MonedaQuery
    {
        public static string SelectMonedas(string esquema)
        {
            return "SELECT" +
                "   mon.CODIGO_MONEDA codigo," +
                "   mon.DESCRIPCION_MONEDA descripcion" +
                " FROM VIMACOOP.CONF_MONEDAS mon" +
                " WHERE mon.ESTADO = '1'";
        }
    }
}
