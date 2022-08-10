namespace Catalogo.Infrastructure.Queries
{
    public class TiposBienesMueblesQuery
    {
        public static string ObtenerTiposBienesMuebles(string esquema)
        {
            return "SELECT " +
                   "STBM.TIPO_BIEN_MUEBLE codigo, " +
                   "STBM.DESCRIPCION descripcion " +
                   $"FROM {esquema}.SIFV_TIPOS_BIENES_MUEBLES STBM " +
                   "WHERE STBM.ESTADO = '1' ORDER BY STBM.ORDEN";
        }
    }
}