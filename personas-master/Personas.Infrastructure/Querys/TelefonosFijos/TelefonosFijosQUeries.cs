using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Querys.TelefonosFijos
{
    public static class TelefonosFijosQueries
    {

        public static string ObtenerCountUltimoNumeroRegistro(string esquema)
        {
            return $"SELECT COALESCE(MAX(NUMERO_REGISTRO), 0) " +
                   $"FROM {esquema}.PERS_TELEFONOS_FIJO  WHERE CODIGO_PERSONA = :codigoPersona ";
        }


        public static string GuardarTelefonoFijo(string esquema)
        {
            return $"INSERT INTO {esquema}.PERS_TELEFONOS_FIJO ( " +
                    $"CODIGO_DIRECCION, " +
                    $"CODIGO_PERSONA, " +
                    $"NUMERO, " +
                    $"CODIGO_OPERADORA, " +
                    $"OBSERVACIONES, " +
                    $"NUMERO_REGISTRO, " +
                    $"ESTADO, " +
                    $"CODIGO_USUARIO_ACTUALIZA, " +
                    $"FECHA_USUARIO_ACTUALIZA) " +
                    $"VALUES( " +
                    $":codigoDireccion, " +
                    $":codigoPersona, " +
                    $":numero, " +
                    $":codigoOperadora, " +
                    $":observaciones, " +
                    $":numeroRegistro," +
                    $" '1', " +
                    $":codigoUsuarioActualiza, " +
                    $":fechaUsuarioActualiza ) ";

        }

        public static string ActualizarTelefonoFijo(string esquema)
        {
            return $"UPDATE {esquema}.PERS_TELEFONOS_FIJO SET " +
            $"NUMERO= :numero, " +
            $"CODIGO_OPERADORA= :codigoOperadora, " +
            $"OBSERVACIONES= :observaciones, " +
            $"CODIGO_DIRECCION= :codigoDireccion, " +
            $"CODIGO_USUARIO_ACTUALIZA= :codigoUsuarioActualiza, " +
            $"FECHA_USUARIO_ACTUALIZA= :fechaUsuarioActualiza " +
            $"WHERE CODIGO_PERSONA= :codigoPersona " +
            $"AND NUMERO_REGISTRO= :numeroRegistro " +
            $"AND ESTADO = '1' ";
        }

        public static string EliminarTelefonoFijo(string esquema)
        {
            return $@"UPDATE {esquema}.PERS_TELEFONOS_FIJO pd SET
					pd.ESTADO = '0',
					pd.CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza,
					pd.FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza
					WHERE
					pd.CODIGO_PERSONA = :codigoPersona AND
					pd.NUMERO_REGISTRO = :numeroRegistro";
        }

        public static string ObtenerTelefonosFijos(string esquema)
        {
            return $"SELECT  " +
                    $"ptf.CODIGO_PERSONA codigoPersona, " +
                    $"ptf.NUMERO numero, " +
                    $"sof.CODIGO_OPERADORA codigoOperadora, " +
                    $"sof.NOMBRE operadora, " +
                    $"ptf.CODIGO_DIRECCION codigoDireccion, " +
                    $"ptf.NUMERO_REGISTRO numeroRegistro " +
                    $"FROM {esquema}.PERS_TELEFONOS_FIJO ptf  " +
                    $"INNER JOIN SIFV_OPERADORAS_FIJO sof ON ptf.CODIGO_OPERADORA = sof.CODIGO_OPERADORA " +
                    $"WHERE ptf.CODIGO_PERSONA = :codigoPersona AND ptf.ESTADO = '1' ";
        }

        public static string ObtenerTelefonoFijo(string esquema)
        {
            return $"SELECT   " +
                    $"ptf.CODIGO_PERSONA codigoPersona,  " +
                    $"ptf.NUMERO numero,  " +
                    $"sof.CODIGO_OPERADORA codigoOperadora,  " +
                    $"sof.NOMBRE operadora,  " +
                    $"pd.CODIGO_DIRECCION codigoDireccion, " +
                    $"pd.CODIGO_PARROQUIA codigoParroquia, " +
                    $"pd.SECTOR sector, " +
                    $"pd.CALLE_PRINCIPAL callePrincipal, " +
                    $"pd.CALLE_SECUNDARIA calleSecundaria " +
                    $"FROM {esquema}.PERS_TELEFONOS_FIJO ptf " +
                    $"INNER JOIN SIFV_OPERADORAS_FIJO sof ON ptf.CODIGO_OPERADORA = sof.CODIGO_OPERADORA " +
                    $"INNER JOIN PERS_DIRECCIONES pd ON ptf.CODIGO_DIRECCION = pd.CODIGO_DIRECCION " +
                    $"WHERE ptf.CODIGO_PERSONA = :codigoPersona " +
                    $"AND ptf.NUMERO_REGISTRO = :numeroRegistro AND ptf.ESTADO = '1'";
        }


    }
}
