using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Querys.ReferenciasComerciales
{
    public static class ReferenciasComercialesQueries
    {

        private static string tabla = "PERS_REFERENCIAS_COMERCIALES";

        public static string obtenerTabla()
        {
            return tabla;
        }

        public static string obtenerReferenciasComerciales(string esquema)
        {
            return $"SELECT " +
            $"NUMERO_REGISTRO as numeroRegistro, " +
            $"CODIGO_PERSONA as codigoPersona, " +
            $"CODIGO_PAIS AS codigoPais, " +
            $"ESTABLECIMIENTO as establecimiento, " +
            $"TELEFONO as telefono, " +
            $"FECHA_RELACION as fechaRelacion " +
            $"FROM {esquema}.{tabla} " +
            $"WHERE CODIGO_PERSONA=:codigoPersona " +
            $"AND ESTADO='1' " +
            $"ORDER BY FECHA_RELACION DESC " +
            $"OFFSET :indiceInicial ROWS FETCH NEXT :numeroRegistros ROWS ONLY ";
        }

        public static string obtenerReferenciaComercial(string esquema)
        {
            return $"SELECT " +
            $"NUMERO_REGISTRO as numeroRegistro, " +
            $"CODIGO_PERSONA as codigoPersona, " +
            $"CODIGO_PAIS as codigoPais, " +
            $"CODIGO_PROVINCIA as codigoProvincia, " +
            $"CODIGO_CIUDAD as codigoCiudad, " +
            $"CODIGO_PARROQUIA as codigoParroquia, " +
            $"ESTABLECIMIENTO as establecimiento, " +
            $"FECHA_RELACION as fechaRelacion, " +
            $"MONTO_CREDITO as montoCredito, " +
            $"PLAZO as plazo, " +
            $"CODIGO_TIPO_TIEMPO as codigoTipoTiempo, " +
            $"TELEFONO as telefono, " +
            $"ESTADO as estado, " +
            $"CODIGO_USUARIO_ACTUALIZA as codigoUsuarioActualiza, " +
            $"FECHA_USUARIO_ACTUALIZA as fechaUsuarioActualiza " +
            $"FROM {esquema}.{tabla} " +
            $"WHERE NUMERO_REGISTRO = :numeroRegistro " +
            $"AND CODIGO_PERSONA = :codigoPersona " +
            $"AND ESTADO='1' ";
        }

        public static string guardarReferenciaComercial(string esquema)
        {
            return $"INSERT INTO {esquema}.{tabla} ( " +
            $"NUMERO_REGISTRO, " +
            $"CODIGO_PERSONA, " +
            $"CODIGO_PAIS, " +
            $"CODIGO_PROVINCIA, " +
            $"CODIGO_CIUDAD, " +
            $"CODIGO_PARROQUIA, " +
            $"ESTABLECIMIENTO, " +
            $"FECHA_RELACION, " +
            $"MONTO_CREDITO, " +
            $"PLAZO, " +
            $"CODIGO_TIPO_TIEMPO, " +
            $"TELEFONO, " +
            $"ESTADO, " +
            $"CODIGO_USUARIO_ACTUALIZA, " +
            $"FECHA_USUARIO_ACTUALIZA ) " +
            $"VALUES " +
            $"(:numeroRegistro, " +
            $":codigoPersona, " +
            $":codigoPais, " +
            $":codigoProvincia, " +
            $":codigoCiudad, " +
            $":codigoParroquia, " +
            $":establecimiento, " +
            $":fechaRelacion, " +
            $":montoCredito, " +
            $":plazo, " +
            $":codigoTipoTiempo, " +
            $":telefono, " +
            $"'1', " +
            $":codigoUsuarioActualiza, " +
            $":fechaUsuarioActualiza ) ";
        }

        public static string actualizarReferenciaComercial(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} SET " +
            $"CODIGO_PAIS=:codigoPais, " +
            $"CODIGO_PROVINCIA=:codigoProvincia, " +
            $"CODIGO_CIUDAD=:codigoCiudad, " +
            $"CODIGO_PARROQUIA=:codigoParroquia, " +
            //$"ESTABLECIMIENTO=:establecimiento, " +
            //$"FECHA_RELACION=:fechaRelacion, " +
            $"MONTO_CREDITO=:montoCredito, " +
            $"PLAZO=:plazo, " +
            $"CODIGO_TIPO_TIEMPO=:codigoTipoTiempo, " +
            $"TELEFONO=:telefono, " +
            $"CODIGO_USUARIO_ACTUALIZA=:codigoUsuarioActualiza, " +
            $"FECHA_USUARIO_ACTUALIZA=:fechaUsuarioActualiza " +
            $"WHERE NUMERO_REGISTRO = :numeroRegistro " +
            $"AND CODIGO_PERSONA = :codigoPersona " +
            $"AND ESTADO='1' ";
        }

        public static string eliminarReferenciaComercial(string esquema)
        {
            return $"UPDATE {esquema}.{tabla} SET " +
            $"ESTADO=:estado, " +
            $"CODIGO_USUARIO_ACTUALIZA=:codigoUsuarioActualiza, " +
            $"FECHA_USUARIO_ACTUALIZA=:fechaUsuarioActualiza " +
            $"WHERE NUMERO_REGISTRO = :numeroRegistro " +
            $"AND CODIGO_PERSONA = :codigoPersona " +
            $"AND ESTADO='1' ";
        }

        public static string obtenerNuevoCodigo(string esquema)
        {
            return $@"SELECT
                COALESCE(MAX(prc.NUMERO_REGISTRO), 0)
                FROM {esquema}.PERS_REFERENCIAS_COMERCIALES prc
                WHERE prc.CODIGO_PERSONA = :codigoPersona";
        }
    }
}
