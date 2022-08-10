namespace Personas.Infrastructure.Querys.BienesInmuebles
{
    public class BienesInmueblesQueries
    {
        public static string ObtenerNumeroRegistroMax(string esquema)
        {
            return "SELECT " +
                   "nvl(max(PBI.NUMERO_REGISTRO), 0) " +
                   $"FROM {esquema}.PERS_BIENES_INMUEBLES PBI";
        }

        public static string ObtenerBienesInmuebles(string esquema)
        {
            return $"SELECT " +
                   $"PBI.NUMERO_REGISTRO numeroRegistro, " +
                   $"PBI.SECTOR sector, " +
                   $"PBI.CODIGO_PERSONA codigoPersona, " +
                   $"stbi.DESCRIPCION tipoBienInmueble, " +
                   $"PBI.DESCRIPCION descripcion, " +
                   $"PBI.CALLE_PRINCIPAL callePrincipal, " +
                   $"PBI.CALLE_SECUNDARIA calleSecundaria " +
                   $"FROM {esquema}.PERS_BIENES_INMUEBLES PBI " +
                   $"INNER JOIN {esquema}.SIFV_TIPOS_BIENES_INMUEBLES stbi " +
                   $"ON stbi.TIPO_BIEN_INMUEBLE = PBI.TIPO_BIEN_INMUEBLE ";
        }

        public static string ObtenerBienesInmueblesSinJoin(string esquema)
        {
            return "SELECT " +
                   "PBI.NUMERO numero, " +
                   "PBI.NUMERO_REGISTRO  numeroRegistro, " +
                   $"PBI.CODIGO_PAIS codigoPais, " +
                   $"PBI.CODIGO_PROVINCIA codigoProvincia , " +
                   $"PBI.CODIGO_CIUDAD codigoCiudad, " +
                   "PBI.CODIGO_PARROQUIA codigoParroquia, " +
                   "PBI.CODIGO_PERSONA codigoPersona, " +
                   "PBI.SECTOR sector, " +
                   "PBI.CALLE_PRINCIPAL callePrincipal, " +
                   "PBI.CALLE_SECUNDARIA calleSecundaria, " +
                   "PBI.DESCRIPCION descripcion " +
                   $"FROM {esquema}.PERS_BIENES_INMUEBLES PBI " +
                   "WHERE PBI.CODIGO_PERSONA = :codigoPersona AND PBI.ESTADO = '1'";
        }

        public static string ObtenerBienInmueble(string esquema)
        {
            return "SELECT " +
                   "PBI.NUMERO_REGISTRO  numeroRegistro, " +
                   "PBI.TIPO_BIEN_INMUEBLE tipoBienInmueble, " +
                   "PBI.CODIGO_PAIS codigoPais, " +
                   "PBI.CODIGO_PROVINCIA codigoProvincia, " +
                   "PBI.CODIGO_CIUDAD    codigoCiudad, " +
                   "PBI.CODIGO_PARROQUIA codigoParroquia, " +
                   "PBI.SECTOR           sector, " +
                   "PBI.CALLE_PRINCIPAL  callePrincipal, " +
                   "PBI.CALLE_SECUNDARIA calleSecundaria, " +
                   "PBI.COMUNIDAD comunidad, " +
                   "PBI.DESCRIPCION descripcion, " +
                   "PBI.NUMERO           numero, " +
                   "PBI.CODIGO_POSTAL codigoPostal, " +
                   "PBI.TIPO_SECTOR tipoSector, " +
                   "PBI.ES_MARGINAL esMarginal, " +
                   "PBI.LONGITUD longitud, " +
                   "PBI.LATITUD latitud, " +
                   "PBI.AVALUO_COMERCIAL avaluoComercial, " +
                   "PBI.AVALUO_CATASTRAL avaluoCatastral, " +
                   "PBI.AREA_TERRENO areaTerreno, " +
                   "PBI.AREA_CONSTRUCCION areaConstruccion, " +
                   "PBI.VALOR_TERRENO_M2 valorTerrenoMetrosCuadrados, " +
                   "PBI.FECHA_CONSTRUCCION fechaConstruccion, " +
                   "PBI.REFERENCIA referencia " +
                   $"FROM {esquema}.PERS_BIENES_INMUEBLES PBI " +
                   "WHERE PBI.CODIGO_PERSONA = :codigoPersona AND  PBI.NUMERO_REGISTRO = :numeroRegistro AND PBI.ESTADO = '1'";
        }

        public static string GuardarBienesInmuebles(string esquema)
        {
            return $"INSERT INTO {esquema}.PERS_BIENES_INMUEBLES ( " +
                   "CODIGO_PERSONA, " +
                   "NUMERO_REGISTRO, " +
                   "TIPO_BIEN_INMUEBLE, " +
                   "CODIGO_PAIS, " +
                   "CODIGO_PROVINCIA, " +
                   "CODIGO_CIUDAD, " +
                   "CODIGO_PARROQUIA, " +
                   "SECTOR, " +
                   "CALLE_PRINCIPAL, " +
                   "CALLE_SECUNDARIA, " +
                   "NUMERO, " +
                   "CODIGO_POSTAL, " +
                   "TIPO_SECTOR, " +
                   "ES_MARGINAL, " +
                   "LONGITUD, " +
                   "LATITUD, " +
                   "AVALUO_COMERCIAL, " +
                   "AVALUO_CATASTRAL, " +
                   "AREA_TERRENO, " +
                   "AREA_CONSTRUCCION, " +
                   "VALOR_TERRENO_M2, " +
                   "FECHA_CONSTRUCCION, " +
                   "REFERENCIA, " +
                   "COMUNIDAD, " +
                   "DESCRIPCION, " +
                   "CODIGO_USUARIO_ACTUALIZA, " +
                   "FECHA_USUARIO_ACTUALIZA, " +
                   "ESTADO )" +
                   "VALUES (" +
                   ":codigoPersona, " +
                   ":numeroRegistro, " +
                   ":tipoBienInmueble, " +
                   ":codigoPais, " +
                   ":codigoProvincia, " +
                   ":codigoCiudad, " +
                   ":codigoParroquia, " +
                   ":sector, " +
                   ":callePrincipal, " +
                   ":calleSecundaria, " +
                   ":numero, " +
                   ":codigoPostal, " +
                   ":tipoSector, " +
                   ":esMarginal, " +
                   ":longitud, " +
                   ":latitud, " +
                   ":avaluoComercial, " +
                   ":avaluoCatastral, " +
                   ":areaTerreno, " +
                   ":areaConstruccion, " +
                   ":valorTerrenoMetrosCuadrados, " +
                   ":fechaConstruccion, " +
                   ":referencia, " +
                   ":comunidad, " +
                   ":descripcion, " +
                   ":codigoUsuarioActualiza, " +
                   ":fechaUsuarioActualiza, " +
                   ":estado)";
        }

        public static string ActualizarBienesInmuebles(string esquema)
        {
            return $"UPDATE PERS_BIENES_INMUEBLES SET " +
                   "TIPO_BIEN_INMUEBLE = :tipoBienInmueble, " +
                   "CALLE_PRINCIPAL = :callePrincipal ," +
                   "CALLE_SECUNDARIA = :calleSecundaria ," +
                   "AVALUO_COMERCIAL = :avaluoComercial ," +
                   "AVALUO_CATASTRAL = :avaluoCatastral ," +
                   "AREA_CONSTRUCCION = :areaConstruccion ," +
                   "VALOR_TERRENO_M2 = :valorTerrenoMetrosCuadrados ," +
                   "FECHA_CONSTRUCCION = :fechaConstruccion ," +
                   "REFERENCIA = :referencia," +
                   "COMUNIDAD = :comunidad ," +
                   "DESCRIPCION = :descripcion, " +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro AND ESTADO = '1' ";
        }

        public static string EliminarBienesInmuebles(string esquema)
        {
            return $"update {esquema}.PERS_BIENES_INMUEBLES set " +
                   "ESTADO = '0'," +
                   "CODIGO_USUARIO_ACTUALIZA = :codigoUsuarioActualiza, " +
                   "FECHA_USUARIO_ACTUALIZA = :fechaUsuarioActualiza " +
                   "WHERE CODIGO_PERSONA = :codigoPersona AND NUMERO_REGISTRO = :numeroRegistro  ";
        }
    }
}