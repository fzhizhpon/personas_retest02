namespace Catalogo.Infrastructure.Queries
{
    public class TiposBienesIntangiblesQuery
    {
        public static string ObtenerTiposBienesIntangibles(string esquema)
        {
            return "SELECT " +
                   "STBI.TIPO_BIEN_INTANGIBLE codigo, " +
                   "STBI.DESCRIPCION descripcion " +
                   $"FROM {esquema}.SIFV_TIPOS_BIENES_INTANGIBLES STBI " +
                   "WHERE STBI.ESTADO = '1' ORDER BY STBI.ORDEN";
        }
    }
}