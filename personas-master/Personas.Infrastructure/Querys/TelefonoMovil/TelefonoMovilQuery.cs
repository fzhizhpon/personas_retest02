namespace Personas.Infrastructure.Querys.TelefonoMovil
{
    public static class TelefonoMovilQuery
    {
        public static string GuardarTelefonoMovil(string esquema)
        {
            return $"INSERT INTO {esquema}.PERS_TELEFONOS_MOVIL(" +
                   "	NUMERO_REGISTRO," +
                   "	CODIGO_PERSONA, " +
                   "	CODIGO_PAIS, " +
                   "	NUMERO, " +
                   "	CODIGO_OPERADORA, " +
                   "	OBSERVACIONES, " +
                   "	PRINCIPAL," +
                   "	CODIGO_USUARIO_ACTUALIZA," +
                   "	FECHA_USUARIO_ACTUALIZA," +
                   "	ESTADO) " +
                   " VALUES(" +
                   "	:codigoTelefonoMovil," +
                   "	:codigoPersona," +
                   "	:codigoPais," +
                   "	:numero," +
                   "	:codigoOperadora," +
                   "	:observaciones," +
                   "	:principal," +
                   "	:codigoUsuarioActualiza," +
                   "	:fechaUsuarioActualiza," +
                   "	'1')";
        }

        public static string QuitarTelefonoPrincipal(string esquema)
        {
            return $"UPDATE {esquema}.PERS_TELEFONOS_MOVIL SET PRINCIPAL = '0'" +
                   " WHERE CODIGO_PERSONA = :codigoPersona";
        }

        public static string ContarTelefonosPrincipales(string esquema)
        {
            return "SELECT " +
                   "	COUNT(*)" +
                   $" FROM {esquema}.PERS_TELEFONOS_MOVIL ptm" +
                   " WHERE ptm.CODIGO_PERSONA = :codigoPersona" +
                   " AND ptm.ESTADO = '1'" +
                   " AND ptm.PRINCIPAL = '1'";
        }

        public static string ObtenerTelefonosMovil(string esquema)
        {
            return $"SELECT " +
                   $"ptm.NUMERO_REGISTRO codigoTelefonoMovil, " +
                   $"ptm.CODIGO_PAIS codigoPais, " +
                   $"sp.CODIGO_MARCADO_MOVIL || ' - ' || sp.DESCRIPCION descripcion, " +
                   $"ptm.CODIGO_PERSONA codigoPersona, " +
                   $"ptm.NUMERO, " +
                   $"ptm.CODIGO_OPERADORA codigoOperadora, " +
                   $"ptm.OBSERVACIONES, " +
                   $"ptm.PRINCIPAL " +
                   $"FROM {esquema}.PERS_TELEFONOS_MOVIL ptm " +
                   $"INNER JOIN VIMACOOP.SIFV_PAISES sp " +
                   $"ON ptm.CODIGO_PAIS = sp.CODIGO_PAIS " +
                   $"WHERE ptm.CODIGO_PERSONA = :codigoPersona " +
                   $"AND ptm.ESTADO = '1' ";
        }

        public static string ObtenerTelefonoMovil(string esquema)
        {
            return $"SELECT " +
                   $"ptm.NUMERO_REGISTRO codigoTelefonoMovil, " +
                   $"ptm.CODIGO_PAIS codigoPais, " +
                   $"sp.CODIGO_MARCADO_MOVIL || ' - ' || sp.DESCRIPCION descripcion, " +
                   $"ptm.CODIGO_PERSONA codigoPersona, " +
                   $"ptm.NUMERO, " +
                   $"ptm.CODIGO_OPERADORA codigoOperadora, " +
                   $"ptm.OBSERVACIONES, " +
                   $"ptm.PRINCIPAL " +
                   $"FROM {esquema}.PERS_TELEFONOS_MOVIL ptm " +
                   $"INNER JOIN VIMACOOP.SIFV_PAISES sp " +
                   $"ON ptm.CODIGO_PAIS = sp.CODIGO_PAIS " +
                   $"WHERE ptm.CODIGO_PERSONA = :codigoPersona " +
                   $"AND ptm.NUMERO = :numero " +
                   $"AND ptm.ESTADO = '1' ";
        }

        public static string ActualizarTelefonoMovil(string esquema)
        {
            return $"UPDATE {esquema}.PERS_TELEFONOS_MOVIL SET " +
                   "	CODIGO_OPERADORA = :codigoOperadora, " +
                   "	OBSERVACIONES = :observaciones, " +
                   "	PRINCIPAL = :principal," +
                   "	CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza," +
                   "	FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza" +
                   " WHERE NUMERO_REGISTRO = :codigoTelefonoMovil" +
                   " AND CODIGO_PERSONA = :codigoPersona";
        }

        public static string EliminarTelefonoMovil(string esquema)
        {
            return $"UPDATE {esquema}.PERS_TELEFONOS_MOVIL SET " +
                   "	CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza," +
                   "	FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza," +
                   "	ESTADO = '0'" +
                   " WHERE NUMERO_REGISTRO = :codigoTelefonoMovil" +
                   " AND CODIGO_PERSONA = :codigoPersona";
        }

        public static string obtenerNuevoCodigo(string esquema)
        {
            return $@"SELECT
                COALESCE(MAX(prc.NUMERO_REGISTRO), 0)
                FROM {esquema}.PERS_TELEFONOS_MOVIL prc
                WHERE prc.CODIGO_PERSONA = :codigoPersona";
        }

        public static string esUnTelefonoMovilEliminado(string esquema)
        {
            return $"SELECT * FROM {esquema}.PERS_TELEFONOS_MOVIL " +
                   "WHERE " +
                   "CODIGO_PERSONA = :codigoPersona AND " +
                   "NUMERO = :numero AND " +
                   "ESTADO= '0'";
        }

        public static string reactivarTelefonoMovil(string esquema)
        {
            return $"UPDATE {esquema}.PERS_TELEFONOS_MOVIL SET " +
                   "CODIGO_OPERADORA = :codigoOperadora, " +
                   "OBSERVACIONES = :observaciones, " +
                   "PRINCIPAL = :principal," +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza," +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
                   "ESTADO = '1' " +
                   " WHERE CODIGO_PERSONA = :codigoPersona" +
                   " AND NUMERO = :numero";
        }
    }
}