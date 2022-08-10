namespace Personas.Infrastructure.Querys.Familiares
{
    public static class FamiliaresQueries
    {

        private static string tabla = "PERS_FAMILIARES";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string GuardarFamiliar(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} (  " +
            $"    CODIGO_PERSONA_FAMILIAR,   " +
            $"    CODIGO_PERSONA,   " +
            $"    CODIGO_PARENTESCO,   " +
            $"    ESTADO,   " +
            $"    OBSERVACION,   " +
            $"    CODIGO_USUARIO_ACTUALIZA,   " +
            $"    FECHA_USUARIO_ACTUALIZA,   " +
            $"    ES_CARGA_FAMILIAR)   " +
            $"VALUES(  " +
            $"    :codigoPersonaFamiliar,   " +
            $"    :codigoPersona,   " +
            $"    :codigoParentesco,   " +
            $"    :estado,   " +
            $"    :observacion,   " +
            $"    :codigoUsuarioActualiza,   " +
            $"    :fechaUsuarioActualiza,   " +
            $"    :esCargaFamiliar)  ";
        }

        public static string ActualizarFamiliar(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
            $"SET CODIGO_PARENTESCO = :codigoParentesco, " +
            $"ESTADO = :estado, " +
            $"OBSERVACION = :observacion, " +
            $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
            $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
            $"ES_CARGA_FAMILIAR = :esCargaFamiliar " +
            $"WHERE CODIGO_PERSONA_FAMILIAR = :codigoPersonaFamiliar " +
            $"AND CODIGO_PERSONA = :codigoPersona ";
        }

        public static string EliminarFamiliar(string esquema)
        {
            return $"UPDATE {esquema}.{tabla}  " +
            $"SET ESTADO= '0'  " +
            $"WHERE CODIGO_PERSONA_FAMILIAR= :codigoPersonaFamiliar  " +
            $"AND CODIGO_PERSONA= :codigoPersona ";
        }

        public static string ObtenerFamiliaresJoin(string esquema)
        {
            return $@"SELECT
                    fam.CODIGO_PERSONA_FAMILIAR codigoPersonaFamiliar,
                    fam.CODIGO_PERSONA codigoPersona,
                    fam.CODIGO_PARENTESCO codigoParentesco,
                    ppn.NOMBRES nombres,
                    ppn.APELLIDO_PATERNO apellidoPaterno,
                    ppn.APELLIDO_MATERNO apellidoMaterno
                    FROM {esquema}.PERS_FAMILIARES fam
                    INNER JOIN {esquema}.PERS_PERSONAS_NATURALES ppn
                    ON fam.CODIGO_PERSONA_FAMILIAR = ppn.CODIGO_PERSONA
                    WHERE fam.CODIGO_PERSONA = :codigoPersona
                    AND fam.ESTADO = '1'";
        }

        public static string ObtenerFamiliarJoin(string esquema)
        {
            return $@"SELECT
                    pf.CODIGO_PERSONA_FAMILIAR AS codigoPersonaFamiliar,  
                    pf.CODIGO_PERSONA AS codigoPersona,
                    pf.CODIGO_PARENTESCO AS codigoParentesco,
                    pf.OBSERVACION AS observacion, 
                    pf.ES_CARGA_FAMILIAR AS esCargaFamiliar, 
                    ppn.NOMBRES AS nombres, 
                    ppn.APELLIDO_PATERNO AS apellidoPaterno, 
                    ppn.APELLIDO_MATERNO AS apellidoMaterno
                    FROM {esquema}.PERS_FAMILIARES pf
                    INNER JOIN {esquema}.PERS_PERSONAS_NATURALES ppn
                    ON ppn.CODIGO_PERSONA = pf.CODIGO_PERSONA_FAMILIAR  
                    WHERE
                    pf.CODIGO_PERSONA = :codigoPersona AND 
                    pf.CODIGO_PERSONA_FAMILIAR = :codigoPersonaFamiliar AND 
                    pf.ESTADO = '1'";
        }

        public static string ObtenerFamiliarEliminado(string esquema)
        {
            return $"SELECT  " +
            $"CODIGO_PERSONA_FAMILIAR codigoPersonaFamiliar,  " +
            $"CODIGO_PERSONA codigoPersona,  " +
            $"CODIGO_PARENTESCO codigoParentesco,  " +
            $"ESTADO estado,  " +
            $"OBSERVACION observacion,  " +
            $"CODIGO_USUARIO_ACTUALIZA codigoUsuarioActualiza,  " +
            $"FECHA_USUARIO_ACTUALIZA fechaUsuarioActualiza,  " +
            $"ES_CARGA_FAMILIAR  esCargaFamiliar  " +
            $"FROM {esquema}.{tabla}  " +
            $"WHERE CODIGO_PERSONA_FAMILIAR= :codigoPersonaFamiliar  " +
            $"AND CODIGO_PERSONA= :codigoPersona  " +
            $"AND ESTADO = '0' ";
        }


    }
}
