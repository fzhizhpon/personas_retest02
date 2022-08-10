namespace Catalogo.Infrastructure.Queries
{
    public static class SectorEconomicoQuery
    {
        public static string SelectSectoresEconomicos(string esquema)
        {
            return "SELECT" +
                "   se.CODIGO_SECTOR_ECONOMICO codigo," +
                "   se.DESCRIPCION" +
                $" FROM {esquema}.SIFV_SECTORES_ECONOMICOS se" +
                " ORDER BY CODIGO_SECTOR_ECONOMICO";
        }
    }
}
