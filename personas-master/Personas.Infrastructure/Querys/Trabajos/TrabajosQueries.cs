namespace Personas.Infrastructure.Querys.Trabajos
{
    public static class TrabajosQueries
    {
        private static string tabla = "PERS_TRABAJOS";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string obtenerTrabajos(string esquema)
        {
            return $@"SELECT
	                    pt.NUMERO_REGISTRO AS codigoTrabajo,
	                    pt.CODIGO_PERSONA AS codigoPersona,
	                    pt.RAZON_SOCIAL AS razonSocial,
	                    pt.CARGO AS cargo,
                        pt.PRINCIPAL AS principal,
	                    pt.FECHA_INGRESO AS fechaIngreso,
                        pt.INGRESOS_MENSUALES AS ingresosMensuales,
	                    sct.DESCRIPCION AS descripcionCategoria
                    FROM {esquema}.{tabla} pt
                    INNER JOIN {esquema}.SIFV_CATEGORIAS_TRABAJOS sct ON pt.CODIGO_CATEGORIA = sct.CODIGO_CATEGORIA
                    WHERE
                    pt.CODIGO_PERSONA = :codigoPersona AND 
                    pt.ESTADO = '1'";
        }

        public static string obtenerTrabajo(string esquema)
        {
            return $"SELECT " +
                   $"NUMERO_REGISTRO AS codigoTrabajo, " +
                   $"CODIGO_PERSONA AS codigoPersona, " +
                   $"CODIGO_ACTIVIDAD AS codigoActividad, " +
                   $"CODIGO_CATEGORIA AS codigoCategoria, " +
                   $"PRINCIPAL AS principal, " +
                   $"CODIGO_PAIS AS codigoPais, " +
                   $"CODIGO_PROVINCIA AS codigoProvincia, " +
                   $"CODIGO_CIUDAD AS codigoCiudad, " +
                   $"CODIGO_PARROQUIA AS codigoParroquia, " +
                   $"INGRESOS_MENSUALES AS ingresosMensuales, " +
                   $"FECHA_INGRESO AS fechaIngreso, " +
                   $"RAZON_SOCIAL AS razonSocial, " +
                   $"DIRECCION AS direccion, " +
                   $"CARGO AS cargo " +
                   $"FROM {esquema}.{tabla} " +
                   $"WHERE NUMERO_REGISTRO = :codigoTrabajo " +
                   $"AND CODIGO_PERSONA = :codigoPersona " +
                   $"AND ESTADO = '1' ";
        }

        public static string eliminarTrabajo(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} SET " +
                   $"ESTADO = '0', " +
                   $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   $"WHERE NUMERO_REGISTRO = :codigoTrabajo " +
                   $"AND CODIGO_PERSONA = :codigoPersona " +
                   $"AND ESTADO = '1' ";
        }

        public static string guardarTrabajo(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} ( " +
                   $"NUMERO_REGISTRO, " +
                   $"CODIGO_PERSONA, " +
                   $"CODIGO_ACTIVIDAD, " +
                   $"CODIGO_CATEGORIA, " +
                   $"PRINCIPAL, " +
                   $"CODIGO_PAIS, " +
                   $"CODIGO_PROVINCIA, " +
                   $"CODIGO_CIUDAD, " +
                   $"CODIGO_PARROQUIA, " +
                   $"INGRESOS_MENSUALES, " +
                   $"FECHA_INGRESO, " +
                   $"RAZON_SOCIAL, " +
                   $"DIRECCION, " +
                   $"CARGO, " +
                   $"CODIGO_USUARIO_ACTUALIZA, " +
                   $"ESTADO, " +
                   $"FECHA_USUARIO_ACTUALIZA ) " +
                   $"VALUES (  " +
                   $":codigoTrabajo, " +
                   $":codigoPersona, " +
                   $":codigoActividad, " +
                   $":codigoCategoria, " +
                   $":principal, " +
                   $":codigoPais, " +
                   $":codigoProvincia, " +
                   $":codigoCiudad, " +
                   $":codigoParroquia, " +
                   $":ingresosMensuales, " +
                   $":fechaIngreso, " +
                   $":razonSocial, " +
                   $":direccion, " +
                   $":cargo, " +
                   $":codigoUsuarioActualiza, " +
                   $"'1', " +
                   $":fechaUsuarioActualiza ) ";
        }

        public static string actualizarTrabajo(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
                   $"SET " +
                   $"CODIGO_CATEGORIA = :codigoCategoria, " +
                   $"FECHA_INGRESO = :fechaIngreso, " +
                   $"RAZON_SOCIAL = :razonSocial, " +
                   $"DIRECCION = :direccion, " +
                   $"CARGO = :cargo, " +
                   $"CODIGO_PAIS = :codigoPais, " +
                   $"CODIGO_PROVINCIA = :codigoProvincia, " +
                   $"CODIGO_CIUDAD = :codigoCiudad, " +
                   $"CODIGO_PARROQUIA = :codigoParroquia, " +
                   $"INGRESOS_MENSUALES = :ingresosMensuales, " +
                   $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
                   $"PRINCIPAL = :principal " +
                   $"WHERE NUMERO_REGISTRO = :codigoTrabajo " +
                   $"AND CODIGO_PERSONA = :codigoPersona " +
                   $"AND ESTADO = '1' ";
        }

        public static string obtenerNuevoCodigo(string esquema)
        {
            return $@"SELECT
                COALESCE(MAX(t.NUMERO_REGISTRO), 0)
                FROM {esquema}.{tabla} t
                WHERE t.CODIGO_PERSONA = :codigoPersona";
        }

        public static string obtenerTrabajoCodigoPersonaRazonSocial(string esquema)
        {
            return $"SELECT " +
                   $"NUMERO_REGISTRO AS codigoTrabajo, " +
                   $"CODIGO_PERSONA AS codigoPersona, " +
                   $"CODIGO_ACTIVIDAD AS codigoActividad, " +
                   $"CODIGO_CATEGORIA AS codigoCategoria, " +
                   $"PRINCIPAL AS principal, " +
                   $"CODIGO_PAIS AS codigoPais, " +
                   $"CODIGO_PROVINCIA AS codigoProvincia, " +
                   $"CODIGO_CIUDAD AS codigoCiudad, " +
                   $"CODIGO_PARROQUIA AS codigoParroquia, " +
                   $"INGRESOS_MENSUALES AS ingresosMensuales, " +
                   $"FECHA_INGRESO AS fechaIngreso, " +
                   $"RAZON_SOCIAL AS razonSocial, " +
                   $"DIRECCION AS direccion, " +
                   $"CARGO AS cargo " +
                   $"FROM {esquema}.{tabla} " +
                   $"WHERE RAZON_SOCIAL = :razonSocial " +
                   $"AND CODIGO_PERSONA = :codigoPersona " +
                   $"AND ESTADO = '1' ";
        }

        public static string esUnTrabajoEliminado(string esquema)
        {
            return $"SELECT * FROM {esquema}.{tabla} " +
                   "WHERE " +
                   "CODIGO_PERSONA= :codigoPersona AND " +
                   "RAZON_SOCIAL= :razonSocial AND " +
                   "ESTADO= '0'";
        }

        public static string reactivarTrabajo(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
                   $"SET " +
                   $"CODIGO_CATEGORIA = :codigoCategoria, " +
                   $"FECHA_INGRESO = :fechaIngreso, " +
                   $"RAZON_SOCIAL = :razonSocial, " +
                   $"DIRECCION = :direccion, " +
                   $"CARGO = :cargo, " +
                   $"CODIGO_PAIS = :codigoPais, " +
                   $"CODIGO_PROVINCIA = :codigoProvincia, " +
                   $"CODIGO_CIUDAD = :codigoCiudad, " +
                   $"CODIGO_PARROQUIA = :codigoParroquia, " +
                   $"INGRESOS_MENSUALES = :ingresosMensuales, " +
                   $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
                   $"PRINCIPAL = :principal, " +
                   $"ESTADO = '1' " +
                   $"WHERE RAZON_SOCIAL = :razonSocial " +
                   $"AND CODIGO_PERSONA = :codigoPersona";
        }
    }
}