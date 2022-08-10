namespace Catalogo.Infrastructure.Queries
{
    public class ProductosQuery
    {
        public static string ObtenerProductosActividadFinanciera(string schema)
        {
            return "SELECT prod.CODIGO_PRODUCTO codigo, " +
                   "prod.DESCRIPCION descripcion " +
                   $"FROM {schema}.CONF_PRODUCTOS prod " +
                   "WHERE prod.CODIGO_MONEDA = :codigoMoneda "+
                   "and prod.CODIGO_ACT_FINANCIERA = :codigoActividadFinanciera " +
                   "and prod.ESTADO = 'A'";
        }

        public static string ObtenerProductosCodigoGrupo(string schema)
        {
            return "SELECT prod.CODIGO_PRODUCTO codigo, " +
                   "prod.DESCRIPCION descripcion " +
                   $"FROM {schema}.CONF_PRODUCTOS prod " +
                   "WHERE prod.CODIGO_MONEDA = :codigoMoneda " +
                   "and prod.CODIGO_GRUPO = :codigoGrupo " +
                   "and prod.ESTADO = 'A'";
        }

    }
}