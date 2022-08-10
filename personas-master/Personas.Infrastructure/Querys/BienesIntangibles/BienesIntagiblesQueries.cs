namespace Personas.Infrastructure.Querys.BienesIntangibles
{
    public class BienesIntagiblesQueries
    {
        public static string ObtenerNumeroRegistroMax(string esquema)
        {
            return "SELECT " +
                   "nvl(max(PBI.NUMERO_REGISTRO), 0) " +
                   $"FROM {esquema}.PERS_BIENES_INTANGIBLES PBI";
        }

        public static string ObtenerBienesIntangibles(string esquema)
        {
            return "SELECT " +
                   "PBI.NUMERO_REGISTRO numeroRegistro, " +
                   "PBI.TIPO_BIEN_INTANGIBLE tipoBienIntangible, " +
                   "PBI.CODIGO_REFERENCIA codigoReferencia, " +
                   "PBI.DESCRIPCION descripcion, " +
                   "PBI.AVALUO_COMERCIAL avaluoComercial " +
                   $"from {esquema}.PERS_BIENES_INTANGIBLES PBI " +
                   "where PBI.CODIGO_PERSONA = :codigoPersona  and PBI.ESTADO = '1'";
        }

        public static string ObtenerBienIntangible(string esquema)
        {
            return "SELECT " +
                   "PBI.NUMERO_REGISTRO numeroRegistro, " +
                   "PBI.TIPO_BIEN_INTANGIBLE tipoBienIntangible, " +
                   "PBI.CODIGO_REFERENCIA codigoReferencia, " +
                   "PBI.DESCRIPCION descripcion, " +
                   "PBI.AVALUO_COMERCIAL avaluoComercial " +
                   $"from {esquema}.PERS_BIENES_INTANGIBLES PBI " +
                   "where PBI.CODIGO_PERSONA = :codigoPersona and PBI.NUMERO_REGISTRO = :numeroRegistro  and PBI.ESTADO = '1'";
        }

        public static string GuardarBienesIntagibles(string esquema)
        {
            return $"INSERT INTO {esquema}.PERS_BIENES_INTANGIBLES (" +
                   "CODIGO_PERSONA, " +
                   "NUMERO_REGISTRO, " +
                   "TIPO_BIEN_INTANGIBLE, " +
                   "CODIGO_REFERENCIA, " +
                   "DESCRIPCION, " +
                   "AVALUO_COMERCIAL," +
                   "CODIGO_USUARIO_ACTUALIZA," +
                   "FECHA_USUARIO_ACTUALIZA," +
                   "ESTADO) " +
                   "VALUES (" +
                   ":codigoPersona, " +
                   ":numeroRegistro, " +
                   ":tipoBienIntangible, " +
                   ":codigoReferencia, " +
                   ":descripcion, " +
                   ":avaluoComercial, " +
                   ":codigoUsuarioActualiza, " +
                   ":fechaUsuarioActualiza, " +
                   ":estado)";
        }

        public static string ActualizarBienesIntangibles(string esquema)
        {
            return $"UPDATE {esquema}.PERS_BIENES_INTANGIBLES SET " +
                   "DESCRIPCION = :descripcion, " +
                   "AVALUO_COMERCIAL = :avaluoComercial, " +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro AND ESTADO = '1' ";
        }

        public static string EliminarBienesIntangibles(string esquema)
        {
            return $"update {esquema}.PERS_BIENES_INTANGIBLES set " +
                   "ESTADO = '0'," +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro  ";
        }
    }
}