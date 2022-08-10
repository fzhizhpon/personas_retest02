namespace Catalogo.Infrastructure.Queries
{
    public class NombreCargosQuery
    {
        public static string ObtenerNombreCargos(string esquema)
        {
            return "SELECT " +
                "nc.CODIGO_CARGO codigo, " +
                "nc.DESCRIPCION descripcion " +
                $"FROM {esquema}.NOM_CARGOS nc" +
                " ORDER BY nc.CODIGO_CARGO";
        }
    }
}