namespace Catalogo.Infrastructure.Queries
{
    public class TablasComunesDetallesQueries
    {
        public static string ObtenerTablasComunesDetallesQueries(string esquema)
        {
            return "SELECT " +
                   "SDTC.CODIGO_ELEMENTO codigo, " +
                   "SDTC.DESCRIPCION_ELEMENTO descripcion " +
                   $"FROM {esquema}.SIFV_DETALLE_TABLAS_COMUNES SDTC " +
                   "WHERE SDTC.CODIGO_TABLA = :codigoTabla AND SDTC.ESTADO = '1' ORDER BY SDTC.ORDEN";
        }
    }
}