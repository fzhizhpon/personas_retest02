namespace Catalogo.Infrastructure.Queries
{
    public static class OperadoraMovilQuery
    {
        public static string SelectOperadorasMoviles(string esquema)
        {
            return "SELECT" +
                "   om.CODIGO_OPERADORA codigo," +
                "   om.NOMBRE descripcion" +
                $" FROM {esquema}.SIFV_OPERADORAS_MOVIL om" +
                " WHERE om.ESTADO = '1'";
        }

        public static string SelectPaisesMarcadoMoviles(string esquema)
        {
            return $"SELECT " +
            $"sp.CODIGO_PAIS codigo, " +
            $"sp.CODIGO_MARCADO_MOVIL || ' - ' || sp.DESCRIPCION descripcion " +
            $"FROM {esquema}.SIFV_PAISES sp ";
        }
    }
}
