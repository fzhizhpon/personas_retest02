namespace Catalogo.Infrastructure.Queries
{
    public class TipoAutenticacionQuery
    {
        public static string ObtenerTipoAutenticacion(string esquema)
        {
            return "SELECT " +
                "sta.CODIGO_TIPO_AUTENTICACION codigo, " +
                "sta.DESCRIPCION descripcion " +
                $"FROM {esquema}.SEG_TIPOS_AUTENTICACION sta " +
                "ORDER BY sta.ORDEN" ;
        }
    }
}