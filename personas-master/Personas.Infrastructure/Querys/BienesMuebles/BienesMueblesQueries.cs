namespace Personas.Infrastructure.Querys.BienesMuebles
{
    public class BienesMueblesQueries
    {
        public static string ObtenerNumeroRegistroMax(string esquema)
        {
            return "SELECT " +
                   "nvl(max(PBM.NUMERO_REGISTRO), 0) " +
                   $"FROM {esquema}.PERS_BIENES_MUEBLES PBM";
        }

        public static string ObtenerBienesMuebles(string esquema)
        {
            return "SELECT " +
                   "PBM.NUMERO_REGISTRO numeroRegistro, " +
                   "PBM.TIPO_BIEN_MUEBLE tipoBienMueble, " +
                   "PBM.CODIGO_REFERENCIA codigoReferencia, " +
                   "PBM.DESCRIPCION descripcion, " +
                   "PBM.AVALUO_COMERCIAL avaluoComercial " +
                   $"FROM {esquema}.PERS_BIENES_MUEBLES PBM " +
                   "WHERE CODIGO_PERSONA = :codigoPersona and PBM.ESTADO = '1'";
        }

        public static string ObtenerBienMueble(string esquema)
        {
            return "SELECT " +
                   "PBM.NUMERO_REGISTRO numeroRegistro, " +
                   "PBM.TIPO_BIEN_MUEBLE tipoBienMueble, " +
                   "PBM.CODIGO_REFERENCIA codigoReferencia, " +
                   "PBM.DESCRIPCION descripcion, " +
                   "PBM.AVALUO_COMERCIAL avaluoComercial " +
                   $"FROM {esquema}.PERS_BIENES_MUEBLES PBM " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro AND PBM.ESTADO = '1'";
        }

        public static string GuardarBienesMuebles(string esquema)
        {
            return $"INSERT INTO {esquema}.PERS_BIENES_MUEBLES (" +
                   "CODIGO_PERSONA, " +
                   "NUMERO_REGISTRO, " +
                   "TIPO_BIEN_MUEBLE, " +
                   "CODIGO_REFERENCIA, " +
                   "DESCRIPCION, " +
                   "AVALUO_COMERCIAL," +
                   "CODIGO_USUARIO_ACTUALIZA," +
                   "FECHA_USUARIO_ACTUALIZA," +
                   "ESTADO) " +
                   "VALUES (" +
                   ":codigoPersona, " +
                   ":numeroRegistro, " +
                   ":tipoBienMueble, " +
                   ":codigoReferencia, " +
                   ":descripcion, " +
                   ":avaluoComercial, " +
                   ":codigoUsuarioActualiza, " +
                   ":fechaUsuarioActualiza, " +
                   ":estado)";
        }

        public static string ActualizarBienesMuebles(string esquema)
        {
            return $"UPDATE {esquema}.PERS_BIENES_MUEBLES SET " +
                   "TIPO_BIEN_MUEBLE = :tipoBienMueble, " +
                   "CODIGO_REFERENCIA = :codigoReferencia, " +
                   "DESCRIPCION = :descripcion, " +
                   "AVALUO_COMERCIAL = :avaluoComercial, " +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro AND ESTADO = '1' ";
        }

        public static string EliminarBienesMuebles(string esquema)
        {
            return $"update {esquema}.PERS_BIENES_MUEBLES set " +
                   "ESTADO = '0', " +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro  ";
        }
    }
}