namespace Catalogo.Infrastructure.Queries
{
    public class TiposBienesInmueblesQuery
    {
        public static string ObtenerTiposBienesInmuebles(string esquema)
        {
            return "SELECT " +
                   "STBI.TIPO_BIEN_INMUEBLE codigo, " +
                   "STBI.DESCRIPCION descripcion " +
                   $"FROM {esquema}.SIFV_TIPOS_BIENES_INMUEBLES STBI " +
                   "WHERE STBI.ESTADO = '1' ORDER BY STBI.ORDEN";
        }
    }
}