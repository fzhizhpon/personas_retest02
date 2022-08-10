namespace Personas.Infrastructure.Querys.ReferenciasPersonales
{
    public static class ReferenciasPersonalesQueries
    {
        private static string tabla = "PERS_REFERENCIAS_PERSONALES";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string obtenerReferenciasPersonales(string esquema)
        {
            return $"SELECT " +
                   $"pers_nat.CODIGO_PERSONA as codigoPersonaReferida, " +
                   $"pers_nat.NOMBRES AS nombres, " +
                   $"pers_nat.APELLIDO_PATERNO AS apellidoPaterno, " +
                   $"pers_nat.APELLIDO_MATERNO AS apellidoMaterno, " +
                   $"refPers.NUMERO_REGISTRO as numeroRegistro, " +
                   $"refPers.FECHA_CONOCE as fechaConoce, " +
                   $"refPers.CODIGO_PERSONA as codigoPersona " +
                   $"FROM {esquema}.{tabla} refPers " +
                   $"INNER JOIN {esquema}.pers_personas_naturales pers_nat ON " +
                   $"pers_nat.codigo_persona = refpers.codigo_persona_referida " +
                   $"WHERE refPers.CODIGO_PERSONA = :codigoPersona " +
                   $"AND refPers.CODIGO_PERSONA_REFERIDA = refpers.codigo_persona_referida " +
                   $"AND refPers.ESTADO = :estado " +
                   $"ORDER BY refpers.NUMERO_REGISTRO " +
                   $"OFFSET :indiceInicial ROWS FETCH NEXT :numeroRegistros ROWS ONLY ";
        }

        public static string obtenerReferenciaPersonalJoin(string esquema)
        {
            return $"SELECT " +
                   $"pers.NUMERO_IDENTIFICACION AS identificacion, " +
                   $"pers.CODIGO_TIPO_IDENTIFICACION AS codigoTipoIdentificacion, " +
                   $"pers_nat.NOMBRES AS nombres, " +
                   $"pers_nat.APELLIDO_PATERNO AS apellidoPaterno, " +
                   $"pers_nat.APELLIDO_MATERNO AS apellidoMaterno, " +
                   $"refPers.NUMERO_REGISTRO as numeroRegistro, " +
                   $"refPers.CODIGO_PERSONA_REFERIDA as codigoPersonaReferida, " +
                   $"refPers.CODIGO_PERSONA as codigoPersona, " +
                   $"refPers.FECHA_CONOCE as fechaConoce, " +
                   $"refPers.OBSERVACIONES as observaciones " +
                   $"FROM {esquema}.{tabla} refPers " +
                   $"INNER JOIN {esquema}.pers_personas_naturales pers_nat ON " +
                   $"pers_nat.codigo_persona = refpers.codigo_persona_referida " +
                   $"INNER JOIN {esquema}.pers_personas pers ON " +
                   $"pers.CODIGO_PERSONA = pers_nat.CODIGO_PERSONA " +
                   $"WHERE refPers.CODIGO_PERSONA = :codigoPersona " +
                   $"AND refPers.CODIGO_PERSONA_REFERIDA = :codigoPersonaReferida " +
                   $"AND refPers.ESTADO = :estado ";
        }

        public static string obtenerReferenciaPersonal(string esquema)
        {
            return $"SELECT " +
                   $"NUMERO_REGISTRO as numeroRegistro, " +
                   $"CODIGO_PERSONA_REFERIDA as codigoPersonaReferida, " +
                   $"CODIGO_PERSONA as codigoPersona, " +
                   $"FECHA_CONOCE as fechaConoce, " +
                   $"OBSERVACIONES as observaciones, " +
                   $"CODIGO_USUARIO_ACTUALIZA as codigoUsuarioActualiza, " +
                   $"FECHA_USUARIO_ACTUALIZA as fechaUsuarioActualiza " +
                   $"FROM {esquema}.{tabla} " +
                   $"WHERE CODIGO_PERSONA = :codigoPersona " +
                   $"AND CODIGO_PERSONA_REFERIDA = :codigoPersonaReferida " +
                   $"AND ESTADO = :estado ";
        }

        public static string eliminarReferenciaPersonal(string esquema)
        {
            return $@"UPDATE {esquema}.{tabla} pd SET
					pd.ESTADO = '0',
					pd.CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
					pd.FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza
					WHERE
					pd.CODIGO_PERSONA = :codigoPersona AND
					pd.CODIGO_PERSONA_REFERIDA = :codigoPersonaReferida";
        }

        public static string guardarReferenciaPersonal(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} ( " +
                   $"CODIGO_PERSONA_REFERIDA, " +
                   $"NUMERO_REGISTRO, " +
                   $"CODIGO_PERSONA, " +
                   $"FECHA_CONOCE, " +
                   $"OBSERVACIONES, " +
                   $"ESTADO, " +
                   $"CODIGO_USUARIO_ACTUALIZA, " +
                   $"FECHA_USUARIO_ACTUALIZA ) " +
                   $"VALUES( " +
                   $":codigoPersonaReferida, " +
                   $":numeroRegistro, " +
                   $":codigoPersona, " +
                   $":fechaConoce, " +
                   $":observaciones, " +
                   $":estado, " +
                   $":codigoUsuarioActualiza, " +
                   $":fechaUsuarioActualiza ) ";
        }

        public static string obtenerNuevoCodigo(string esquema)
        {
            return $@"SELECT COALESCE(MAX(prc.NUMERO_REGISTRO), 0)
                FROM {esquema}.{tabla} prc
                WHERE prc.CODIGO_PERSONA = :codigoPersona";
        }


        public static string esUnaReferenciaPersonalEliminada(string esquema)
        {
            return $"SELECT * FROM {esquema}.{tabla} " +
                   "WHERE " +
                   "CODIGO_PERSONA_REFERIDA = :codigoPersonaReferida AND " +
                   "CODIGO_PERSONA = :codigoPersona AND " +
                   "ESTADO= '0'";
        }


        public static string reactivarReferenciaPersonal(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
                   "SET FECHA_CONOCE = :fechaConoce," +
                   "OBSERVACIONES = :observaciones," +
                   "ESTADO = '1', " +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE " +
                   "CODIGO_PERSONA_REFERIDA = :codigoPersonaReferida AND " +
                   "CODIGO_PERSONA = :codigoPersona";
        }
    }
}