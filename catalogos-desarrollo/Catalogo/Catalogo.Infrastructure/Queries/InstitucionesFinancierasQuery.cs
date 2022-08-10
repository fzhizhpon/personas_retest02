namespace Catalogo.Infrastructure.Queries
{
    public class InstitucionesFinancierasQuery
    {
        public static string ObtenerInstitucionesFinancieras(string esquema)
        {
            return "SELECT sif.CODIGO codigo, " +
                   "sif.NOMBRE_FINANCIERA descripcion " +
                   $"FROM {esquema}.sifv_instituciones_financieras sif " +
                   "WHERE sif.TIPO_FINANCIERA = :codigoTipoFinanciera and sif.ESTADO = '1'";
        }

        public static string ObtenerInsitucionesFinancierasFull(string esquema)
        {
            return "SELECT sif.CODIGO codigo, " +
                   "sif.NOMBRE_FINANCIERA descripcion " +
                   $"FROM {esquema}.sifv_instituciones_financieras sif ";
        }
        
    }
}