using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Querys.RelacionInstitucion
{
    public static class RelacionInstitucionQueries
    {
        private static string Tabla = "PERS_PERSONAS_RELACION_INSTITUCION";

        public static string obtenerData(string esquema)
        {
            return $@"SELECT 
                    pi.codigo_persona AS codigoPersona, 
                    pi.codigo_relacion AS codigoRelacion, 
                    ri.descripcion AS descripcion, 
                    pi.fecha_asignacion AS fechaAsignacion,
                    pi.codigo_agencia_registra AS codigoAgenciaRegistra
                    from {esquema}.{Tabla} pi 
                    left join sifv_tipos_relacion_institucion ri 
                    ON PI.CODIGO_RELACION = RI.CODIGO_RELACION 
                    where codigo_persona = :codigoPersona
                    AND ri.ESTADO = '1' ";
        }

        public static string obtenerRelacionPersonaMin(string esquema)
        {
            return $@"SELECT 
                    pri.codigo_persona AS codigoPersona, 
                    pri.codigo_relacion AS codigoRelacion 
                    from {esquema}.{Tabla} pri 
                    where pri.codigo_persona = :codigoPersona AND
                    pri.CODIGO_RELACION = :codigoRelacion";
        }

        public static string InsertarPersonaRelacionInstitucion(string esquema)
        {
            return $@"insert into {esquema}.{Tabla}
                        (CODIGO_RELACION, 
                        CODIGO_PERSONA, 
                        FECHA_ASIGNACION, 
                        USUARIO_ASIGNA,
                        CODIGO_ASIGNADO, 
                        ESTADO, 
                        CODIGO_AGENCIA_REGISTRA
                        )VALUES(
                        :codigoRelacion,
                        :codigoPersona,
                        :fechaAsignacion,
                        :usuarioAsigna,
                        :codigoAsignado,
                        :estado,
                        :codigoAgenciaRegistra)";
        }

        
        public static string CambiarEstadoPersonaRelacionInstitucion(string esquema)
        {
            return $@"UPDATE {esquema}.{Tabla} pri SET
					pri.ESTADO = :estado
					WHERE
					pri.CODIGO_PERSONA = :codigoPersona AND
					pri.CODIGO_RELACION = :codigoRelacion";
        }



    }
}
