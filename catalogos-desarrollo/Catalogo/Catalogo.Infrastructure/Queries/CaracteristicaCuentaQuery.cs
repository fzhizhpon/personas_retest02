namespace Catalogo.Infrastructure.Queries
{
    public static class CaracteristicaCuentaQuery
    {
        public static string SelectCaracteristicaCuenta(string esquema)
        {
            return "SELECT" +
                "   cc.CODIGO_CARACTERISTICA codigo," +
                "   cc.DESCRIPCION_CANAL descripcion" +
                $" FROM {esquema}.CAPTA_CARACTERISTICAS_CUENTAS cc" +
                " WHERE cc.ESTADO = '1' " +
                "order by cc.ORDEN";
        }
    }
}
