namespace Catalogo.Infrastructure.Queries
{
    public class DepartamentosQuery
    {
        public static string ObtenerDepartamentos(string esquema)
        {
            return "SELECT " +
                "nd.DEP_CODIGO codigo, " +
                "nd.DEP_DESCRIPCION descripcion " +
                $"FROM {esquema}.NOM_DEPARTAMENTOS nd";
        }
    }
}