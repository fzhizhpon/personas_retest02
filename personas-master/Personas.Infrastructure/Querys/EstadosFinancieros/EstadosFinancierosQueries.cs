namespace Personas.Infrastructure.Querys.EstadosFinancieros
{
    public static class EstadosFinancierosQueries
    {

        public static string GuardarEstadoFinanciero(string esquema)
        {
            return $"INSERT INTO " +
                $"{esquema}.PERS_ESTADOS_FINANCIEROS ( " +
                $"CODIGO_PERSONA, " +
                $"CUENTA_CONTABLE, " +
                $"VALOR, " +
                $"CODIGO_USUARIO_ACTUALIZA, " +
                $"FECHA_USUARIO_ACTUALIZA, " +
                $"OBSERVACION " +
                $") VALUES ( " +
                $":codigoPersona, " +
                $":cuentaContable, " +
                $":valor, " +
                $":codigoUsuarioActualiza, " +
                $":fechaUsuarioActualiza, " +
                $":observacion " +
                $") ";
        }

        public static string ActualizarEstadoFinanciero(string esquema)
        {
            return $"UPDATE {esquema}.PERS_ESTADOS_FINANCIEROS " +
                    $"SET  " +
                    $"VALOR = :valor, " +
                    $"OBSERVACION = :observacion, " +
                    $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                    $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                    $"WHERE CODIGO_PERSONA = :codigoPersona " +
                    $"AND CUENTA_CONTABLE = :cuentaContable ";
        }

        public static string ObtenerCuentasEstadoFinanciero(string esquema)
        {
            return @$"SELECT 
                    scef.CUENTA_CONTABLE AS cuentaContable, 
                    scef.DESCRIPCION AS descripcion, 
                    scef.QUERY AS query,
                    scef.CODIGO_COMPONENTE AS codigoComponente,
                    pef.VALOR AS valor,
                    pef.OBSERVACION AS observacion 
                    FROM {esquema}.SIFV_CUENTAS_ESTADO_FINANCIERO scef 
                    LEFT JOIN {esquema}.PERS_ESTADOS_FINANCIEROS pef ON 
                    pef.CUENTA_CONTABLE = scef.CUENTA_CONTABLE AND 
                    pef.CODIGO_PERSONA = :codigoPersona 
                    WHERE 
                    scef.TIPO_CUENTA = :tipoCuenta 
                    AND scef.MAYOR_AUXILIAR = 1 
                    AND scef.ESTADO = 'A' 
                    ORDER BY scef.CUENTA_CONTABLE";
        }

    }

}
