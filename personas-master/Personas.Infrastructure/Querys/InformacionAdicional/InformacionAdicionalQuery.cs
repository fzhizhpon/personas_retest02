namespace Personas.Infrastructure.Querys.TablasComunes
{
    public class InformacionAdicionalQuery
    {
        public static string ObtenerInformacionAdicional(string esquema)
        {
            return "SELECT " +
                   "SDTC.CODIGO_ELEMENTO codigoElemento, " +
                   "SDTC.DESCRIPCION_ELEMENTO descripcionElemento, " +
                   "sd.OBSERVACION observacion, " +
                   "COALESCE(sd.ESTADO, '0') estado " +
                   $"FROM {esquema}.SIFV_DETALLE_TABLAS_COMUNES SDTC " +
                   $"LEFT JOIN {esquema}.SIFV_DETALLE_INF_ADIC sd " +
                   "ON SDTC.CODIGO_TABLA = sd.CODIGO_TABLA AND " +
                   "sd.CODIGO_REFERENCIA = :codigoReferencia AND " +
                   "sd.CODIGO_ELEMENTO = SDTC.CODIGO_ELEMENTO " +
                   "WHERE SDTC.CODIGO_TABLA = :codigoTabla " +
                   "ORDER BY SDTC.ORDEN ";
        }

        public static string GuardarInformacionAdicional(string esquema)
        {
            return "INSERT INTO " +
                   $"{esquema}.SIFV_DETALLE_INF_ADIC ( " +
                   "CODIGO_REFERENCIA, " +
                   "CODIGO_TABLA, " +
                   "CODIGO_ELEMENTO, " +
                   "OBSERVACION," +
                   "ESTADO," +
                   "CODIGO_MODULO ) VALUES (" +
                   ":codigoReferencia, " +
                   ":codigoTabla, " +
                   ":codigoElemento," +
                   ":observacion," +
                   ":estado," +
                   ":codigoModulo)";
        }

        public static string ActualizarInformacionAdicional(string esquema)
        {
            return $"update {esquema}.SIFV_DETALLE_INF_ADIC " +
                   "SET OBSERVACION = :observacion, " +
                   "ESTADO  = :estado " +
                   "WHERE CODIGO_REFERENCIA = :codigoReferencia " +
                   "and CODIGO_TABLA = :codigoTabla " +
                   "and CODIGO_ELEMENTO = :codigoElemento "+
                   "and CODIGO_MODULO = :codigoModulo";
        }
    }
}