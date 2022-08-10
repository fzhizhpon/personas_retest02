namespace Catalogo.Infrastructure.Queries
{
    public static class EmpresaQuery
    {
        public static string SelectEmpresa(string esquema)
        {
            return "SELECT" +
                "   e.CODIGO_EMPRESA codigo," +
                "   e.NOMBRE_CORTO descripcion" +
                $" FROM {esquema}.SIFV_EMPRESAS e" +
                " WHERE CODIGO_EMPRESA = 1";
        }
    }
}
