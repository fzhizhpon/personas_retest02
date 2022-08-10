namespace Personas.Infrastructure.Querys.Personas
{
    public static class PersonaQuery
    {
        private static string tabla = "PERS_PERSONAS";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string ObtenerCodigoPersonaMax(string esquema)
        {
            return $"SELECT" +
                $"	NVL(MAX(p.CODIGO_PERSONA), 0)" +
                $" FROM {esquema}.{tabla} p";
        }

        public static string GuardarPersona(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla}(" +
                $"	CODIGO_PERSONA," +
                $"	NUMERO_IDENTIFICACION," +
                $"	FECHA_REGISTRO," +
                $"	OBSERVACIONES," +
                $"	CODIGO_TIPO_IDENTIFICACION," +
                $"	CODIGO_TIPO_PERSONA," +
                $"	CODIGO_USUARIO_REGISTRA," +
                $"	FECHA_USUARIO_ACTUALIZA," +
                $"	CODIGO_TIPO_CONTRIBUYENTE," +
                $"	CODIGO_AGENCIA)" +
                $" VALUES(" +
                $"	:codigoPersona," +
                $"	:numeroIdentificacion," +
                $"	:fechaUsuarioRegistra," +
                $"	:observaciones," +
                $"	:codigoTipoIdentificacion," +
                $"	:codigoTipoPersona," +
                $"	:codigoUsuarioRegistra," +
                $"	:fechaUsuarioRegistra," +
                $"	:codigoTipoContribuyente," +
                $"	:codigoAgencia) ";
        }

        public static string ObtenerCodigoPersonaPorIdentificacion(string esquema)
        {
            return $"SELECT " +
                $"	p.CODIGO_PERSONA" +
                $" FROM {esquema}.{tabla} p" +
                $" WHERE p.NUMERO_IDENTIFICACION = :numeroIdentificacion";
        }

        public static string ActualizarPersona(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} SET " +
                $"	NUMERO_IDENTIFICACION = :numeroIdentificacion, " +
                $"	OBSERVACIONES = :observaciones, " +
                $"	CODIGO_TIPO_IDENTIFICACION = :codigoTipoIdentificacion, " +
                $"	CODIGO_TIPO_PERSONA = :codigoTipoPersona, " +
                $"	CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioRegistra, " +
                $"	FECHA_USUARIO_ACTUALIZA = :fechaUsuarioRegistra, " +
                $"	CODIGO_TIPO_CONTRIBUYENTE = :codigoTipoContribuyente," +
                $"	CODIGO_AGENCIA = :codigoAgencia" +
                $" WHERE CODIGO_PERSONA = :codigoPersona";
        }

        public static string ObtenerPersona(string esquema)
        {
            return @$"SELECT
	                pp.CODIGO_PERSONA codigoPersona,
	                pp.NUMERO_IDENTIFICACION numeroIdentificacion, 
	                pp.OBSERVACIONES observaciones,
	                pp.CODIGO_TIPO_IDENTIFICACION codigoTipoIdentificacion, 
	                pp.CODIGO_TIPO_PERSONA codigoTipoPersona,
	                pp.CODIGO_USUARIO_ACTUALIZA codigoUsuarioRegistra, 
	                pp.FECHA_USUARIO_ACTUALIZA fechaUsuarioRegistra,
	                pp.CODIGO_TIPO_CONTRIBUYENTE codigoTipoContribuyente,
	                pp.CODIGO_AGENCIA codigoAgencia,
	                dd.ID_DOCUMENTO codigoDocumento
                FROM {esquema}.PERS_PERSONAS pp
                LEFT JOIN {esquema}.DOCU_DIGITALIZADOS dd
                ON pp.CODIGO_PERSONA = dd.CODIGO_PERSONA
                AND dd.CODIGO_COMPONENTE = 1
                AND dd.CODIGO_GRUPO = 35
                AND dd.CODIGO_TIPO_DOCUMENTO = 3
                WHERE pp.CODIGO_PERSONA = :codigoPersona
                AND rownum = 1
                ORDER BY dd.FECHA_VIGENCIA DESC";
        }

        public static string ObtenerPersonas(string esquema)
        {
            return $@"SELECT 
						pp.CODIGO_PERSONA AS codigoPersona, 
						pp.CODIGO_TIPO_PERSONA AS codigoTipoPersona,		
						pp.CODIGO_TIPO_IDENTIFICACION AS codigoTipoIdentificacion,
						pp.NUMERO_IDENTIFICACION AS numeroIdentificacion,
						stp.DESCRIPCION AS descripcionTipoPersona,
						sti.DESCRIPCION AS descripcionTipoIdentificacion, 
						pers.nombre AS nombre
					FROM
					(
						SELECT ppn.CODIGO_PERSONA codigo_persona, 
							   ppn.APELLIDO_PATERNO || ' ' || ppn.APELLIDO_MATERNO || ' ' || ppn.NOMBRES AS nombre 
						FROM {esquema}.PERS_PERSONAS_NATURALES ppn
						union
						SELECT ppnn.CODIGO_PERSONA codigo_persona, ppnn.RAZON_SOCIAL AS nombre 
						FROM {esquema}.PERS_PERSONAS_NO_NATURALES ppnn
					) pers
					INNER JOIN {esquema}.PERS_PERSONAS pp 
					ON pers.codigo_persona = pp.CODIGO_PERSONA 
					INNER JOIN {esquema}.SIFV_TIPOS_IDENTIFICACIONES sti
					ON pp.CODIGO_TIPO_IDENTIFICACION = sti.CODIGO_TIPO_IDENTIFICACION
					INNER JOIN {esquema}.SIFV_TIPOS_PERSONAS stp 
					ON pp.CODIGO_TIPO_PERSONA = stp.CODIGO_TIPO_PERSONA ";
        }

        public static string ObtenerPersonaJoinMinimo(string esquema)
        {
            return $"SELECT pp.CODIGO_PERSONA codigoPersona,  " +
            $"pp.CODIGO_TIPO_PERSONA codigoTipoPersona,  " +
            $"ppn.NOMBRES || ' ' || ppn.APELLIDO_PATERNO || ' ' || ppn.APELLIDO_MATERNO nombre " +
            $"FROM {esquema}.PERS_PERSONAS pp  " +
            $"RIGHT JOIN {esquema}.PERS_PERSONAS_NATURALES ppn  " +
            $"ON pp.CODIGO_PERSONA = ppn.CODIGO_PERSONA  " +
            $"WHERE pp.CODIGO_PERSONA = :codigoPersona " +
            $"UNION " +
            $"SELECT pp.CODIGO_PERSONA codigoPersona,  " +
            $"pp.CODIGO_TIPO_PERSONA codigoTipoPersona,  " +
            $"ppnn.RAZON_SOCIAL nombre FROM {esquema}.PERS_PERSONAS pp  " +
            $"RIGHT JOIN {esquema}.PERS_PERSONAS_NO_NATURALES ppnn  " +
            $"ON pp.CODIGO_PERSONA = ppnn.CODIGO_PERSONA  " +
            $"WHERE pp.CODIGO_PERSONA = :codigoPersona ";
        }

        public static string ColocarFechaUltActualizacion(string esquema)
        {
            return $@"UPDATE {esquema}.PERS_PERSONAS SET
                    FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza,
                    CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza
                    WHERE 
                    CODIGO_PERSONA = :codigoPersona";
        }

        public static string ContarPersonasIdentificacion(string esquema)
        {
            return @"SELECT COUNT(*) FROM PERS_PERSONAS pp WHERE NUMERO_IDENTIFICACION = :nroIdentificacion";
        }
    }
}
