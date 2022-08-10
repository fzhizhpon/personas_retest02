using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Querys.ReferenciasFinancieras
{
    public static class ReferenciasFinancierasQueries
    {

        private static string tabla = "PERS_REFERENCIAS_FINANCIERAS";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string obtenerReferenciasFinancieras(string esquema)
        {
            return $"SELECT " +
            $"stif.CODIGO AS codigoTipoInstitucionFinanciera, " +
            $"fin.NUMERO_REGISTRO AS numeroRegistro, " +
            $"fin.CODIGO_TIPO_CUENTA_FINANCIERA AS codigoTIpoCuentaFinanciera, " +
            $"fin.NUMERO_CUENTA AS numeroCuenta, " +
            $"fin.CODIGO_PERSONA AS codigoPersona, " +
            $"fin.CIFRAS AS cifras, " +
            $"fin.FECHA_CUENTA AS fechaCuenta, " +
            $"fin.OBSERVACIONES AS observaciones, " +
            $"fin.SALDO AS saldo, " +
            $"fin.CODIGO_INSTITUCION_FINANCIERA AS codigoInstitucionFinanciera, " +
            $"inst.NOMBRE_FINANCIERA AS nombreFinanciera, " +
            $"fin.SALDO_OBLIGACION AS saldoObligacion, " +
            $"fin.OBLIGACION_MENSUAL AS obligacionMensual " +
            $"FROM {esquema}.{tabla} fin  " +
            $"INNER JOIN {esquema}.SIFV_INSTITUCIONES_FINANCIERAS inst ON  fin.CODIGO_INSTITUCION_FINANCIERA = inst.CODIGO " +
            $"INNER JOIN {esquema}.SIFV_TIPOS_INST_FINANCIERAS stif ON stif.CODIGO = inst.TIPO_FINANCIERA " +
            $"WHERE fin.CODIGO_PERSONA =:codigoPersona AND fin.ESTADO = '1' " +
            $"ORDER BY fin.NUMERO_REGISTRO ";
        }

        public static string obtenerReferenciaFinanciera(string esquema)
        {

            return $"SELECT " +
            $"stif.CODIGO codigoTipoInstitucionFinanciera, " +
            $"fin.NUMERO_REGISTRO numeroRegistro, " +
            $"fin.CODIGO_TIPO_CUENTA_FINANCIERA codigoTIpoCuentaFinanciera, " +
            $"fin.NUMERO_CUENTA numeroCuenta, " +
            $"fin.CODIGO_PERSONA codigoPersona, " +
            $"fin.CIFRAS cifras, " +
            $"fin.FECHA_CUENTA fechaCuenta, " +
            $"fin.OBSERVACIONES observaciones, " +
            $"fin.SALDO saldo, " +
            $"fin.CODIGO_INSTITUCION_FINANCIERA codigoInstitucionFinanciera, " +
            $"inst.NOMBRE_FINANCIERA nombreFinanciera, " +
            $"fin.SALDO_OBLIGACION saldoObligacion, " +
            $"fin.OBLIGACION_MENSUAL obligacionMensual " +
            $"FROM {esquema}.{tabla} fin  " +
            $"INNER JOIN {esquema}.SIFV_INSTITUCIONES_FINANCIERAS inst ON  fin.CODIGO_INSTITUCION_FINANCIERA = inst.CODIGO " +
            $"INNER JOIN {esquema}.SIFV_TIPOS_INST_FINANCIERAS stif ON stif.CODIGO = inst.TIPO_FINANCIERA " +
            $"WHERE fin.CODIGO_PERSONA =:codigoPersona " +
            $"AND fin.ESTADO = '1' " +
            $"AND fin.NUMERO_REGISTRO=:numeroRegistro " +
            $"ORDER BY fin.NUMERO_REGISTRO  ";

            //return $"SELECT " +
            //$"stif.CODIGO AS codigoTipoInstitucionFinanciera, " +
            //$"fin.NUMERO_REGISTRO AS numeroRegistro, " +
            //$"fin.CODIGO_TIPO_CUENTA_FINANCIERA AS codigoTIpoCuentaFinanciera, " +
            //$"fin.NUMERO_CUENTA AS numeroCuenta, " +
            //$"fin.CODIGO_PERSONA AS codigoPersona, " +
            //$"fin.CIFRAS AS cifras, " +
            //$"fin.FECHA_CUENTA AS fechaCuenta, " +
            //$"fin.OBSERVACIONES AS observaciones, " +
            //$"fin.SALDO AS saldo, " +
            //$"fin.CODIGO_INSTITUCION_FINANCIERA AS codigoInstitucionFinanciera, " +
            //$"inst.INSTITUCION_FINANCIERA AS institucionFinanciera, " +
            //$"fin.SALDO_OBLIGACION AS saldoObligacion, " +
            //$"fin.OBLIGACION_MENSUAL AS obligacionMensual " +
            //$"FROM {esquema}.{tabla} fin  " +
            //$"INNER JOIN {esquema}.SIFV_INSTITUCIONES_FINANCIERAS inst ON  fin.CODIGO_INSTITUCION_FINANCIERA = inst.CODIGO " +
            //$"INNER JOIN {esquema}.SIFV_TIPOS_INST_FINANCIERAS stif ON stif.CODIGO = inst.TIPO_FINANCIERA " +
            //$"WHERE fin.CODIGO_PERSONA =:codigoPersona AND fin.ESTADO = '1' AND fin.NUMERO_REGISTRO=:numeroRegistro " +
            //$"ORDER BY fin.NUMERO_REGISTRO ";
        }

        public static string eliminarReferenciaFinanciera(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} " +
            $"SET ESTADO = '0', " +
            $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
            $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza " +
            $"WHERE NUMERO_REGISTRO=:numeroRegistro " +
            $"AND CODIGO_PERSONA=:codigoPersona ";
        }

        public static string guardarReferenciaFinanciera(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} ( " +
            $"CODIGO_PERSONA, " +
            $"NUMERO_REGISTRO, " +
            $"CODIGO_TIPO_CUENTA_FINANCIERA, " +
            $"NUMERO_CUENTA, " +
            $"CODIGO_INSTITUCION_FINANCIERA, " +
            $"CIFRAS, " +
            $"FECHA_USUARIO_ACTUALIZA, " +
            $"CODIGO_USUARIO_ACTUALIZA, " +
            $"FECHA_CUENTA, " +
            $"ESTADO, " +
            $"SALDO, " +
            $"SALDO_OBLIGACION, " +
            $"OBLIGACION_MENSUAL, " +
            $"OBSERVACIONES ) " +
            $"VALUES( " +
            $":codigoPersona, " +
            $":numeroRegistro, " +
            $":codigoTipoCuentaFinanciera, " +
            $":numeroCuenta, " +
            $":codigoInstitucionFinanciera, " +
            $":cifras, " +
            $":fechaUsuarioActualiza, " +
            $":codigoUsuarioActualiza, " +
            $":fechaCuenta, " +
            $"'1', " +
            $":saldo, " +
            $":saldoObligacion, " +
            $":obligacionMensual, " +
            $":observaciones) ";
        }

        public static string actualizarReferenciaFinanciera(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} SET " +
            //$"CODIGO_TIPO_CUENTA_FINANCIERA = :codigoTipoCuentaFinanciera, " +
            $"CIFRAS = :cifras, " +
            //$"FECHA_CUENTA = :fechaCuenta, " +
            $"SALDO = :saldo, " +
            //$"CODIGO_INSTITUCION_FINANCIERA = :codigoInstitucionFinanciera, " +
            $"SALDO_OBLIGACION = :saldoObligacion, " +
            $"OBLIGACION_MENSUAL = :obligacionMensual, " +
            $"OBSERVACIONES = :observaciones, " +
            $"FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza, " +
            $"CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza " +
            $"WHERE " +
            $"NUMERO_REGISTRO =:numeroRegistro " +
            $"AND CODIGO_PERSONA =:codigoPersona " +
            $"AND ESTADO = '1' ";
        }

        public static string obtenerNuevoCodigo(string esquema)
        {
            return $@"SELECT
                COALESCE(MAX(prc.NUMERO_REGISTRO), 0)
                FROM {esquema}.{tabla} prc
                WHERE prc.CODIGO_PERSONA = :codigoPersona";
        }

    }
}
