namespace Personas.Infrastructure.Querys.Representante
{
    public static class RepresentantesQueries
    {
        private static string tabla = "PERS_REPRESENTANTES";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string guardarRepresentante(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} ( " +
                   $"    CODIGO_PERSONA,  " +
                   $"    CODIGO_PERSONA_REPRES,  " +
                   $"    CODIGO_TIPO_REPRESENTANTE,  " +
                   $"    PRINCIPAL,  " +
                   $"    ESTADO,  " +
                   $"    CODIGO_USUARIO_ACTUALIZA,  " +
                   $"    FECHA_USUARIO_ACTUALIZA)  " +
                   $" VALUES( " +
                   $"    :codigoPersona,  " +
                   $"    :codigoRepresentante,  " +
                   $"    :codigoTipoRepresentante,  " +
                   $"    :principal,  " +
                   $"    :estado,  " +
                   $"    :codigoUsuarioActualiza,  " +
                   $"    :fechaUsuarioActualiza) ";
        }

        public static string eliminarRepresentante(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
                   $"SET  ESTADO= :estado, " +
                   $"CODIGO_USUARIO_ACTUALIZA= :codigoUsuarioActualiza,  " +
                   $"FECHA_USUARIO_ACTUALIZA= :fechaUsuarioActualiza " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante ";
        }

        public static string actualizarRepresentante(string esquema)
        {
            return $"UPDATE {esquema}.{tabla}  " +
                   $"SET  " +
                   $"CODIGO_TIPO_REPRESENTANTE= :codigoTipoRepresentante,  " +
                   $"PRINCIPAL= :principal,  " +
                   $"CODIGO_USUARIO_ACTUALIZA= :codigoUsuarioActualiza,  " +
                   $"FECHA_USUARIO_ACTUALIZA= :fechaUsuarioActualiza " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante " +
                   $"AND ESTADO= :estado ";
        }

        public static string actualizarRepresentantePrincipal(string esquema)
        {
            return $"UPDATE {esquema}.{tabla}  " +
                   $"SET  " +
                   $"PRINCIPAL= '0' " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentante(string esquema)
        {
            return $"SELECT  " +
                   $"CODIGO_PERSONA codigoPersona,  " +
                   $"CODIGO_PERSONA_REPRES codigoRepresentante,  " +
                   $"CODIGO_TIPO_REPRESENTANTE codigoTipoRepresentante,  " +
                   $"PRINCIPAL principal,  " +
                   $"ESTADO estado,  " +
                   $"CODIGO_USUARIO_ACTUALIZA codigoUsuarioActualiza,  " +
                   $"FECHA_USUARIO_ACTUALIZA fechaUsuarioActualiza  " +
                   $"FROM  {esquema}.{tabla}  " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona  " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentanteJoin(string esquema)
        {
            return $"SELECT   " +
                   $"rep.CODIGO_PERSONA codigoPersona,  " +
                   $"rep.CODIGO_PERSONA_REPRES codigoRepresentante, " +
                   $"ppn.NOMBRES nombres, " +
                   $"ppn.APELLIDO_PATERNO apellidoPaterno, " +
                   $"ppn.APELLIDO_MATERNO apellidoMaterno, " +
                   $"rep.CODIGO_TIPO_REPRESENTANTE codigoTipoRepresentante,   " +
                   $"rep.PRINCIPAL principal " +
                   $"FROM {esquema}.{tabla} rep  " +
                   $"INNER JOIN PERS_PERSONAS_NATURALES ppn  " +
                   $"ON rep.CODIGO_PERSONA = ppn.CODIGO_PERSONA  " +
                   $"WHERE rep.CODIGO_PERSONA= :codigoPersona   " +
                   $"AND rep.CODIGO_PERSONA_REPRES= :codigoRepresentante " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentantesJoin(string esquema)
        {
            return $"SELECT   " +
                   $"rep.CODIGO_PERSONA codigoPersona,  " +
                   $"rep.CODIGO_PERSONA_REPRES codigoRepresentante, " +
                   $"pp.NUMERO_IDENTIFICACION numeroIdentificacion, " +
                   $"ppn.NOMBRES nombres, " +
                   $"ppn.APELLIDO_PATERNO apellidoPaterno, " +
                   $"ppn.APELLIDO_MATERNO apellidoMaterno, " +
                   $"rep.CODIGO_TIPO_REPRESENTANTE codigoTipoRepresentante,   " +
                   $"rep.PRINCIPAL principal " +
                   $"FROM {esquema}.{tabla} rep  " +
                   $"INNER JOIN PERS_PERSONAS_NATURALES ppn ON rep.CODIGO_PERSONA_REPRES = ppn.CODIGO_PERSONA  " +
                   $"INNER JOIN PERS_PERSONAS pp ON rep.CODIGO_PERSONA_REPRES = pp.CODIGO_PERSONA " +
                   $"WHERE rep.CODIGO_PERSONA= :codigoPersona " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentantesJoinFiltros(string esquema)
        {
            return $"SELECT   " +
                   $"rep.CODIGO_PERSONA codigoPersona,  " +
                   $"rep.CODIGO_PERSONA_REPRES codigoRepresentante, " +
                   $"pp.NUMERO_IDENTIFICACION numeroIdentificacion, " +
                   $"ppn.NOMBRES nombres, " +
                   $"ppn.APELLIDO_PATERNO apellidoPaterno, " +
                   $"ppn.APELLIDO_MATERNO apellidoMaterno, " +
                   $"rep.CODIGO_TIPO_REPRESENTANTE codigoTipoRepresentante,   " +
                   $"rep.PRINCIPAL principal " +
                   $"FROM {esquema}.{tabla} rep  " +
                   $"INNER JOIN PERS_PERSONAS_NATURALES ppn ON rep.CODIGO_PERSONA_REPRES = ppn.CODIGO_PERSONA  " +
                   $"INNER JOIN PERS_PERSONAS pp ON rep.CODIGO_PERSONA_REPRES = pp.CODIGO_PERSONA ";
        }


        public static string obtenerRepresentantesPrincipales(string esquema)
        {
            return $"SELECT  " +
                   $"CODIGO_PERSONA codigoPersona, " +
                   $"CODIGO_PERSONA_REPRES codigoRepresentante " +
                   $"FROM  {esquema}.{tabla}  " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona  " +
                   $"AND PRINCIPAL = '1' " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentanteMinimo(string esquema)
        {
            return $"SELECT  " +
                   $"CODIGO_PERSONA codigoPersona,  " +
                   $"CODIGO_PERSONA_REPRES codigoRepresentante,  " +
                   $"PRINCIPAL principal  " +
                   $"FROM  {esquema}.{tabla}  " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona  " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante " +
                   $"AND ESTADO = :estado ";
        }

        public static string obtenerRepresentanteCodigoPersonaCodigoRepre(string esquema)
        {
            return $"SELECT * FROM {esquema}.{tabla} " +
                   "WHERE " +
                   $"CODIGO_PERSONA= :codigoPersona AND   " +
                   $"CODIGO_PERSONA_REPRES= :codigoRepresentante AND  " +
                   "ESTADO= '1'";
        }

        public static string esUnRepresetanteEliminado(string esquema)
        {
            return $"SELECT * FROM {esquema}.{tabla} " +
                   "WHERE " +
                   $"CODIGO_PERSONA= :codigoPersona AND  " +
                   $"CODIGO_PERSONA_REPRES= :codigoRepresentante AND " +
                   "ESTADO= '0'";
        }

        public static string reactivarRepresentante(string esquema)
        {
            return $"UPDATE {esquema}.{tabla}  " +
                   $"SET  " +
                   $"CODIGO_TIPO_REPRESENTANTE= :codigoTipoRepresentante,  " +
                   $"PRINCIPAL= :principal,  " +
                   $"CODIGO_USUARIO_ACTUALIZA= :codigoUsuarioActualiza,  " +
                   $"FECHA_USUARIO_ACTUALIZA= :fechaUsuarioActualiza, " +
                   "ESTADO = '1' " +
                   $"WHERE CODIGO_PERSONA= :codigoPersona " +
                   $"AND CODIGO_PERSONA_REPRES= :codigoRepresentante ";
        }
    }
}