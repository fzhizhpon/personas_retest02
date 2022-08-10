namespace Catalogo.Infrastructure.Queries
{
    public class TablasComunesCabeceraQueries
    {


        public static string ObtenerTablasComunesCabecera(string esquema /* ,string codigoOpcion*/)
        {
            return "SELECT " + 
                   "SCTC.CODIGO_TABLA codigo," +
                   "SCTC.DESCRIPCION descripcion " +
                   $"FROM {esquema}.SIFV_CABECERA_TABLAS_COMUNES SCTC " +
                   " WHERE SCTC.ESTADO = 1 " +
                   "AND SCTC.CODIGO_MODULO= :codigoModulo ORDER BY SCTC.ORDEN";
        }
        
    }
}