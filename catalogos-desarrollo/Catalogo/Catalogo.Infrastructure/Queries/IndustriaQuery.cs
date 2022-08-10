namespace Catalogo.Infrastructure.Queries
{
    public static class IndustriaQuery
    {
        public static string SelectIndustriaPorSubSector(string esquema)
        {
            return "SELECT" +
                "   i.CODIGO_INDUSTRIA codigo," +
                "   i.DESCRIPCION" +
                " FROM VIMACOOP.SIFV_INDUSTRIAS_SECTORES_ECONOMICOS i" +
                " WHERE i.CODIGO_SECTOR_ECONOMICO = :sectorEconomico" +
                " AND i.CODIGO_SUBSECTOR_ECONOMICO = :subSectorEconomico";
        }
    }
}
