namespace Catalogo.Infrastructure.Queries
{
    public static class TipoActividadTrabajoQuery
    {
        public static string SelectTiposActividadesTrabajos(string esquema)
        {
            return "SELECT " +
                "   tat.CODIGO," +
                "   tat.DESCRIPCION" +
                $" FROM {esquema}.SIFV_ACTIVIDADES_ECONOMICAS tat";
        }
    }
}
