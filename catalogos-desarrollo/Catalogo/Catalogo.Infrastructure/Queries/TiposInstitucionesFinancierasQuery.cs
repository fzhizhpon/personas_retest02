namespace Catalogo.Infrastructure.Queries
{
    public static class TiposInstitucionesFinancierasQuery
    {
        public static string ObtenerTiposInstitucionesFinancieras(string esquema)
        {
            return "SELECT STIF.CODIGO codigo," +
                   "STIF.DESCRIPCION descripcion " +
                   $"FROM {esquema}.SIFV_TIPOS_INST_FINANCIERAS STIF " +
                   "WHERE STIF.ESTADO = '1'";
        }
    }
}