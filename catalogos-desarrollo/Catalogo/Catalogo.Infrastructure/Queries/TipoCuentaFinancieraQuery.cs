namespace Catalogo.Infrastructure.Queries
{
    public static class TipoCuentaFinancieraQuery
    {
        public static string SelectTiposCuentasFinancieras(string esquema)
        {
            return "SELECT" +
                "   tcf.CODIGO_TIPO_CUENTA_FINANCIERA codigo," +
                "   tcf.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_CUENTAS_FINANCIERAS tcf";
        }
    }
}
