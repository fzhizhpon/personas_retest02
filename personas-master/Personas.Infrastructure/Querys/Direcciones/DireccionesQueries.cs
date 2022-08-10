namespace Personas.Infrastructure.Querys.Direcciones
{
    public static class DireccionesQueries
    {
        public const string Tabla = "PERS_DIRECCIONES";

        public static string InsertarDireccion(string esquema)
        {
            return $@"INSERT INTO {esquema}.{Tabla} (
						CODIGO_PERSONA,
						NUMERO_REGISTRO,
						CODIGO_PAIS,
						CODIGO_PROVINCIA,
						CODIGO_CIUDAD,
						CODIGO_PARROQUIA,
						CALLE_PRINCIPAL,
						CALLE_SECUNDARIA,
						NUMERO_CASA,
						SECTOR,
						CODIGO_POSTAL,
						ES_MARGINAL,
						LATITUD,
						LONGITUD,
						PRINCIPAL,
						ESTADO,
						CODIGO_USUARIO_ACTUALIZA,
						FECHA_USUARIO_ACTUALIZA,
						CODIGO_TIPO_RESIDENCIA,
						COMUNIDAD,
						REFERENCIA,
						TIPO_SECTOR,
						FECHA_INICIAL_RESIDENCIA,
						VALOR_ARRIENDO
					) VALUES (
						:codigoPersona,
						:numeroRegistro,
						:codigoPais,
						:codigoProvincia,
						:codigoCiudad,
						:codigoParroquia,
						:callePrincipal,
						:calleSecundaria,
						:numeroCasa,
						:sector,
						:codigoPostal,
						:esMarginal,
						:latitud,
						:longitud,
						:principal,
						'1',
						:codigoUsuarioActualiza,
						:fechaUsuarioActualiza,
						:codigoTipoResidencia,
						:comunidad,
						:referencia,
						:tipoSector,
						:fechaInicialResidencia,
						:valorArriendo
					)";
        }

        public static string ObtenerCodigoNuevaDireccion(string esquema)
        {
            return $@"SELECT COALESCE(max(pd.NUMERO_REGISTRO), 0)
					FROM {esquema}.{Tabla} pd
					WHERE pd.CODIGO_PERSONA = :codigoPersona";
        }

        public static string QuitarPrincipal(string esquema)
        {
            return $@"UPDATE {esquema}.{Tabla} pd
					SET pd.PRINCIPAL = '0'
					WHERE pd.CODIGO_PERSONA = :codigoPersona";
        }

        public static string ContarDireccionesPrincipales(string esquema)
        {
            return $@"SELECT count(pd.CODIGO_PERSONA)
					FROM {esquema}.{Tabla} pd
					WHERE pd.PRINCIPAL = '1' AND pd.ESTADO = '1'";
        }

        public static string EliminarDireccion(string esquema)
        {
            return $@"UPDATE {esquema}.{Tabla} pd SET
					pd.ESTADO = '0',
					pd.CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
					pd.FECHA_USUARIO_ACTUALIZA = :fechaActualiza
					WHERE
					pd.CODIGO_PERSONA = :codigoPersona AND
					pd.NUMERO_REGISTRO = :numeroRegistro";
        }

        public static string ObtenerDirecciones(string esquema)
        {
            return $@"SELECT 
						pd.NUMERO_REGISTRO AS numeroRegistro,
						pd.CODIGO_PAIS AS codigoPais,
						pd.CODIGO_PROVINCIA AS codigoProvincia,
						pd.CODIGO_CIUDAD AS codigoCiudad,
						pd.CODIGO_PARROQUIA AS codigoParroquia,
						pd.CALLE_PRINCIPAL AS callePrincipal,
						pd.CALLE_SECUNDARIA AS calleSecundaria,
						pd.LONGITUD AS longitud,
						pd.LATITUD AS latitud,
						pd.NUMERO_CASA AS numeroCasa,
						pd.SECTOR AS sector,
						pd.PRINCIPAL AS principal,
						pd.CODIGO_TIPO_RESIDENCIA AS codigoTipoResidencia,
						str.DESCRIPCION AS descripcionTipoResidencia,
						pd.FECHA_INICIAL_RESIDENCIA AS fechaInicialResidencia,
						ptf.NUMERO numeroTelFijo,
						ptf.NUMERO_REGISTRO numeroRegistroTelFijo,
						pd.VALOR_ARRIENDO AS valorArriendo
					FROM {esquema}.{Tabla} pd
                    INNER JOIN {esquema}.PERS_TELEFONOS_FIJO ptf ON ptf.CODIGO_PERSONA = pd.CODIGO_PERSONA 
					INNER JOIN {esquema}.SIFV_TIPOS_RESIDENCIAS str ON pd.CODIGO_TIPO_RESIDENCIA = str.CODIGO 
					WHERE pd.CODIGO_PERSONA = :codigoPersona 
					AND pd.NUMERO_REGISTRO = ptf.CODIGO_DIRECCION
					AND pd.ESTADO = '1'
					ORDER BY pd.PRINCIPAL DESC";
        }

        public static string ObtenerDireccion(string esquema)
        {

            return $"SELECT " +
                    $"pd.NUMERO_REGISTRO numeroRegistro, " +
					$"pd.CODIGO_PERSONA codigoPersona, " +
					$"pd.CODIGO_PAIS codigoPais, " +
                    $"pd.CODIGO_PROVINCIA codigoProvincia, " +
                    $"pd.CODIGO_CIUDAD codigoCiudad, " +
                    $"pd.CODIGO_PARROQUIA codigoParroquia, " +
                    $"pd.CALLE_PRINCIPAL callePrincipal, " +
                    $"pd.CALLE_SECUNDARIA calleSecundaria, " +
                    $"pd.NUMERO_CASA numeroCasa, " +
                    $"pd.SECTOR sector, " +
                    $"pd.CODIGO_POSTAL codigoPostal, " +
                    $"pd.ES_MARGINAL esMarginal, " +
                    $"pd.LATITUD latitud, " +
                    $"pd.LONGITUD longitud, " +
                    $"pd.PRINCIPAL principal, " +
                    $"pd.CODIGO_TIPO_RESIDENCIA codigoTipoResidencia, " +
                    $"pd.COMUNIDAD comunidad, " +
                    $"pd.TIPO_SECTOR tipoSector, " +
                    $"pd.REFERENCIA referencia, " +
                    $"pd.FECHA_INICIAL_RESIDENCIA fechaInicialResidencia, " +
                    $"pd.VALOR_ARRIENDO valorArriendo, " +
					$"ptf.NUMERO_REGISTRO numeroRegistro, " +
					$"ptf.NUMERO numero, " +
                    $"sof.CODIGO_OPERADORA codigoOperadora, " +
					$"ptf.OBSERVACIONES observaciones " +

					$"FROM {esquema}.{Tabla} pd " +
                    $"INNER JOIN {esquema}.PERS_TELEFONOS_FIJO ptf ON ptf.CODIGO_PERSONA = pd.CODIGO_PERSONA  " +
                    $"INNER JOIN {esquema}.SIFV_OPERADORAS_FIJO sof ON sof.CODIGO_OPERADORA = ptf.CODIGO_OPERADORA " +
                    $"WHERE " +
                    $"pd.CODIGO_PERSONA = :codigoPersona AND " +
                    $"pd.NUMERO_REGISTRO = :numeroRegistro AND " +
                    $"ptf.CODIGO_DIRECCION = :numeroRegistro AND " +
                    $"pd.ESTADO = '1'  ";
        }

        public static string ActualizarDireccion(string esquema)
        {
            return $@"UPDATE {esquema}.{Tabla} SET
						CODIGO_PAIS = :codigoPais,
						CODIGO_PROVINCIA = :codigoProvincia,
						CODIGO_CIUDAD = :codigoCiudad,
						CODIGO_PARROQUIA = :codigoParroquia,
						CALLE_PRINCIPAL = :callePrincipal,
						CALLE_SECUNDARIA = :calleSecundaria,
						NUMERO_CASA = :numeroCasa,
						SECTOR = :sector,
						CODIGO_POSTAL = :codigoPostal,
						ES_MARGINAL = :esMarginal,
						LATITUD = :latitud,
						LONGITUD = :longitud,
						PRINCIPAL = :principal,
						CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
						FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza,
						CODIGO_TIPO_RESIDENCIA = :codigoTipoResidencia,
						COMUNIDAD = :comunidad,
						REFERENCIA = :referencia,
						TIPO_SECTOR = :tipoSector,
						FECHA_INICIAL_RESIDENCIA = :fechaInicialResidencia,
						VALOR_ARRIENDO = :valorArriendo
					WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro AND ESTADO = '1' ";
        }
    }
}