namespace Personas.Infrastructure.Querys.CorreosElectronicos
{
	public static class CorreosQueries
	{
		public const string Tabla = "PERS_CORREOS_ELECTRONICOS";

		public static string ObtenerTodos(string esquema)
		{
			return @$"SELECT
					pce.NUMERO_REGISTRO AS codigoCorreoElectronico,
					pce.CORREO_ELECTRONICO AS correoElectronico,
					pce.PRINCIPAL AS esPrincipal,
					pce.OBSERVACIONES AS observaciones,
					pce.CODIGO_PERSONA AS codigoPersona
					FROM {esquema}.{Tabla} pce
					WHERE pce.CODIGO_PERSONA = :codigoPersona AND pce.ESTADO = '1'
					ORDER BY pce.PRINCIPAL DESC";
		}

		public static string AgregarCorreo(string esquema)
        {
			return $@"INSERT INTO {esquema}.{Tabla} (
						NUMERO_REGISTRO,
						CODIGO_PERSONA,
						CORREO_ELECTRONICO,
						PRINCIPAL,
						OBSERVACIONES,
						CODIGO_USUARIO_ACTUALIZA,
						FECHA_USUARIO_ACTUALIZA,
						ESTADO
					) VALUES (
						:codigoCorreoElectronico,
						:codigoPersona,
						:correoElectronico,
						:esPrincipal,
						:observaciones,
						:codigoUsuarioActualiza,
						:fechaActualiza,
						'1'
					)";
		}

		public static string QuitarPrincipal(string esquema)
        {
			return $@"UPDATE {esquema}.{Tabla} pce
					SET pce.PRINCIPAL = '0'
					WHERE pce.CODIGO_PERSONA = :codigoPersona";
        }

		public static string ObtenerNuevoCodigo(string esquema)
        {
			return $@"SELECT COALESCE(max(pce.NUMERO_REGISTRO), 0)
					FROM {esquema}.{Tabla} pce
					WHERE pce.CODIGO_PERSONA = :codigoPersona";
        }

		public static string ContarCorreosPrincipales(string esquema)
        {
			return $@"SELECT count(pce.CODIGO_PERSONA)
					FROM {esquema}.{Tabla} pce
					WHERE pce.PRINCIPAL = '1' AND pce.ESTADO = '1'";
        }

		public static string ActualizarCorreo(string esquema)
        {
			return $@"UPDATE {esquema}.{Tabla} pce SET
					pce.PRINCIPAL = :esPrincipal,
					pce.OBSERVACIONES = :observaciones,
					pce.CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
					pce.FECHA_USUARIO_ACTUALIZA = :fechaActualiza,
					pce.ESTADO = :estado
					WHERE
					pce.CODIGO_PERSONA = :codigoPersona AND
					pce.NUMERO_REGISTRO = :codigoCorreoElectronico";
        }

		public static string ContarCorreosPorNombre(string esquema)
		{
			return $@"SELECT pce.NUMERO_REGISTRO 
					FROM {esquema}.{Tabla} pce
					WHERE
					CODIGO_PERSONA = :codigoPersona AND
					CORREO_ELECTRONICO = :correoElectronico";
		}

		public static string EliminarCorreo(string esquema)
		{
			return $@"UPDATE {esquema}.{Tabla} pce SET
					pce.ESTADO = '0',
					pce.CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
					pce.FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza
					WHERE
					pce.CODIGO_PERSONA = :codigoPersona AND
					pce.NUMERO_REGISTRO = :codigoCorreoElectronico";
		}

		public static string ObtenerCorreoCodigo(string esquema)
        {
			return $@"SELECT
					pce.NUMERO_REGISTRO as codigoCorreoElectronico,
					pce.CODIGO_PERSONA as codigoPersona,
					pce.CORREO_ELECTRONICO as correoElectronico,
					pce.PRINCIPAL as esPrincipal,
					pce.OBSERVACIONES as observaciones,
					pce.ESTADO as estado,
					pce.CODIGO_USUARIO_ACTUALIZA as codigoUsuarioActualiza,
					pce.FECHA_USUARIO_ACTUALIZA as fechaActualiza
					FROM {esquema}.{Tabla} pce
					WHERE
					pce.CODIGO_PERSONA = :codigoPersona AND
					pce.NUMERO_REGISTRO = :codigoCorreoElectronico AND
					pce.ESTADO = '1'";
        }

		public static string ObtenerCorreoNombre(string esquema)
		{
			return $@"SELECT
					pce.NUMERO_REGISTRO as codigoCorreoElectronico,
					pce.CODIGO_PERSONA as codigoPersona,
					pce.CORREO_ELECTRONICO as correoElectronico,
					pce.PRINCIPAL as esPrincipal,
					pce.OBSERVACIONES as observaciones,
					pce.ESTADO as estado,
					pce.CODIGO_USUARIO_ACTUALIZA as codigoUsuarioActualiza,
					pce.FECHA_USUARIO_ACTUALIZA as fechaActualiza
					FROM {esquema}.{Tabla} pce
					WHERE
					pce.CODIGO_PERSONA = :codigoPersona AND
					pce.CORREO_ELECTRONICO = :correoElectronico AND
					pce.ESTADO = '1'";
		}
	}
}

