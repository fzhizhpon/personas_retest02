namespace Catalogo.Infrastructure.Queries
{
    public static class SubSectorEconomicoQuery
    {
        public static string SelectSubSectoresPorSector(string esquema)
        {
            return "SELECT" +
                "   sse.CODIGO_SUBSECTOR_ECONOMICO codigo," +
                "   sse.DESCRIPCION" +
                $" FROM {esquema}.SIFV_SUBSECTORES_ECONOMICOS sse" +
                " WHERE sse.CODIGO_SECTOR_ECONOMICO = :sectorEconomico";
        }
    }
}
