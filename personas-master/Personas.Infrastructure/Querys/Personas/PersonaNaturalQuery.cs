namespace Personas.Infrastructure.Querys.Personas
{
	public static class PersonaNaturalQuery
	{
		private static string tabla = "PERS_PERSONAS_NATURALES";

		public static string obtenerTabla()
		{
			return tabla;
		}

		public static string InfoPersona(string esquema)
        {
            return $@"SELECT
						p.CODIGO_PERSONA codigoPersona,
						ti.DESCRIPCION tipoIdentificacion,
						p.NUMERO_IDENTIFICACION numeroIdentificacion,
						pn.NOMBRES,
						pn.APELLIDO_PATERNO apellidoPaterno,
						pn.APELLIDO_MATERNO apellidoMaterno,
						pn.FECHA_NACIMIENTO fechaNacimiento,
						ce.CORREO_ELECTRONICO correoElectronico,
						dd.ID_DOCUMENTO codigoDocumento,
						p.FECHA_USUARIO_ACTUALIZA AS fechaUltimaActualizacion
					 FROM {esquema}.PERS_PERSONAS p
					 INNER JOIN {esquema}.SIFV_TIPOS_IDENTIFICACIONES ti
						ON ti.CODIGO_TIPO_IDENTIFICACION = p.CODIGO_TIPO_IDENTIFICACION
					 INNER JOIN {esquema}.PERS_PERSONAS_NATURALES pn
						ON pn.CODIGO_PERSONA = p.CODIGO_PERSONA
					 LEFT JOIN {esquema}.PERS_CORREOS_ELECTRONICOS ce
						ON ce.CODIGO_PERSONA = p.CODIGO_PERSONA
						AND ce.PRINCIPAL = '1'
					LEFT JOIN {esquema}.DOCU_DIGITALIZADOS dd
						ON p.CODIGO_PERSONA = dd.CODIGO_PERSONA
						AND dd.CODIGO_COMPONENTE = 1
						AND dd.CODIGO_GRUPO = 35
						AND dd.CODIGO_TIPO_DOCUMENTO = 4
					WHERE p.CODIGO_PERSONA = :codigoPersona
					AND rownum = 1
					ORDER BY dd.FECHA_VIGENCIA DESC";
        }

		public static string FotoCedula(string esqumea)
        {
			return $@"SELECT
					dd.ID_DOCUMENTO AS idDocumento
					FROM DOCU_DIGITALIZADOS dd
					WHERE
					dd.CODIGO_PERSONA = :codigoPersona AND
					dd.CODIGO_COMPONENTE = 1 AND 
					dd.CODIGO_GRUPO = 35 AND 
					dd.CODIGO_TIPO_DOCUMENTO = 4 AND 
					dd.TIPO_ARCHIVO = 'I'
					ORDER BY FECHA_SISTEMA DESC
					FETCH NEXT 2 ROWS ONLY";
        }

		public static string GuardarPersonaNatural(string esquema)
        {
			return $"INSERT INTO {esquema}.{tabla} ( " +
			$"    CODIGO_PERSONA, " +
			$"    NOMBRES, " +
			$"    APELLIDO_PATERNO, " +
			$"    APELLIDO_MATERNO, " +
			$"    FECHA_NACIMIENTO, " +
			$"    TIENE_DISCAPACIDAD, " +
			$"    CODIGO_TIPO_DISCAPACIDAD, " +
			$"    PORCENTAJE_DISCAPACIDAD, " +
			$"    CODIGO_PAIS_NACIMIENTO, " +
			$"    CODIGO_PROVINCIA_NACIMIENTO, " +
			$"    CODIGO_CIUDAD_NACIMIENTO, " +
			$"    CODIGO_PARROQUIA_NACIMIENTO, " +
			$"    CODIGO_TIPO_SANGRE, " +
			$"    CODIGO_CONYUGE, " +
			$"    CODIGO_ESTADO_CIVIL, " +
			$"    CODIGO_GENERO, " +
			$"    CODIGO_PROFESION, " +
			$"    CODIGO_TIPO_ETNIA, " +
			$"    CODIGO_USUARIO_ACTUALIZA, " +
			$"    FECHA_USUARIO_ACTUALIZA, " +
			$"    VULNERABLE " +
			$") VALUES ( " +
			$"    :codigoPersona, " +
			$"    :nombres, " +
			$"    :apellidoPaterno, " +
			$"    :apellidoMaterno, " +
			$"    :fechaNacimiento, " +
			$"    :tieneDiscapacidad, " +
			$"    :codigoTipoDiscapacidad, " +
			$"    :porcentajeDiscapacidad, " +
			$"    :codigoPaisNacimiento, " +
			$"    :codigoProvinciaNacimiento, " +
			$"    :codigoCiudadNacimiento, " +
			$"    :codigoParroquiaNacimiento, " +
			$"    :codigoTipoSangre, " +
			$"    :codigoConyuge, " +
			$"    :codigoEstadoCivil, " +
			$"    :codigoGenero, " +
			$"    :codigoProfesion, " +
			$"    :codigoTipoEtnia, " +
			$"    :codigoUsuarioActualiza, " +
			$"    :fechaUsuarioActualiza, " +
			$"    :vulnerable " +
			$") ";
		}

		public static string ActualizarPersonaNatural(string esquema)
        {
			return $"UPDATE {esquema}.{tabla} SET " +
				"	NOMBRES = :nombres," +
				"	APELLIDO_PATERNO = :apellidoPaterno, " +
				"	APELLIDO_MATERNO = :apellidoMaterno, " +
				"	FECHA_NACIMIENTO = :fechaNacimiento, " +
				"	TIENE_DISCAPACIDAD = :tieneDiscapacidad, " +
				"	CODIGO_TIPO_DISCAPACIDAD = :codigoTipoDiscapacidad, " +
				"	PORCENTAJE_DISCAPACIDAD = :porcentajeDiscapacidad, " +
				"	CODIGO_PAIS_NACIMIENTO = :codigoPaisNacimiento, " +
				"	CODIGO_PROVINCIA_NACIMIENTO = :codigoProvinciaNacimiento, " +
				"	CODIGO_CIUDAD_NACIMIENTO = :codigoCiudadNacimiento, " +
				"	CODIGO_PARROQUIA_NACIMIENTO = :codigoParroquiaNacimiento, " +
				"	CODIGO_TIPO_SANGRE = :codigoTipoSangre," +
				"	CODIGO_CONYUGE = :codigoConyuge, " +
				"	CODIGO_ESTADO_CIVIL = :codigoEstadoCivil, " +
				"	CODIGO_GENERO = :codigoGenero, " +
				"	CODIGO_PROFESION = :codigoProfesion, " +
				"	CODIGO_TIPO_ETNIA = :codigoTipoEtnia, " +
				"	CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
				"	FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
				"	VULNERABLE = :esVulnerable " +
				" WHERE CODIGO_PERSONA = :codigoPersona";
        }

		public static string ObtenerPersonaNatural(string esquema)
        {
			return "SELECT " +
				"	CODIGO_PERSONA codigoPersona," +
				"	NOMBRES nombres, " +
				"	APELLIDO_PATERNO apellidoPaterno," +
				"	APELLIDO_MATERNO apellidoMaterno, " +
				"	FECHA_NACIMIENTO fechaNacimiento," +
				"	TIENE_DISCAPACIDAD tieneDiscapacidad," +
				"	CODIGO_TIPO_DISCAPACIDAD codigoTipoDiscapacidad, " +
				"	PORCENTAJE_DISCAPACIDAD porcentajeDiscapacidad," +
				"	CODIGO_PAIS_NACIMIENTO codigoPaisNacimiento," +
				"	CODIGO_PROVINCIA_NACIMIENTO codigoProvinciaNacimiento, " +
				"	CODIGO_CIUDAD_NACIMIENTO codigoCiudadNacimiento," +
				"	CODIGO_PARROQUIA_NACIMIENTO codigoParroquiaNacimiento, " +
				"	CODIGO_TIPO_SANGRE codigoTipoSangre," +
				"	CODIGO_CONYUGE codigoConyuge," +
				"	CODIGO_ESTADO_CIVIL codigoEstadoCivil, " +
				"	CODIGO_GENERO codigoGenero," +
				"	CODIGO_PROFESION codigoProfesion," +
				"	CODIGO_TIPO_ETNIA codigoTipoEtnia," +
				"	CODIGO_USUARIO_ACTUALIZA codigoUsuarioActualiza, " +
				"	FECHA_USUARIO_ACTUALIZA fechaUsuarioActualiza, " +
				"	VULNERABLE esVulnerable " +
				$" FROM {esquema}.{tabla} WHERE CODIGO_PERSONA = :codigoPersona";
        }

		public static string ActualizarDatosConyuge(string esquema)
        {
			return $@"UPDATE {esquema}.PERS_PERSONAS_NATURALES
				SET
				CODIGO_ESTADO_CIVIL = :codigoEstadoCivil,
				CODIGO_CONYUGE = :codigoPersona
				WHERE
				CODIGO_PERSONA = :codigoConyuge";
        }

		public static string QuitarConyuge(string esquema)
        {
			return $@"UPDATE {esquema}.PERS_PERSONAS_NATURALES
					SET
					CODIGO_ESTADO_CIVIL = 1,
					CODIGO_CONYUGE = null
					WHERE
					CODIGO_CONYUGE = :codigoPersona";
        }

	}
}
