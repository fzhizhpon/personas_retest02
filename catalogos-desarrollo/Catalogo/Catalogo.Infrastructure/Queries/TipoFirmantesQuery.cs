namespace Catalogo.Infrastructure.Queries
{
    public static class TipoFirmanteQuery
    {
        public static string SelectTipoFirmante(string esquema)
        {
            return "SELECT" +
                "   tf.TIPO_FIRM_CODIGO codigo," +
                "   tf.TIPO_FIRM_DESCRIPCION descripcion" +
                $" FROM {esquema}.CAPTA_TIPOS_FIRMANTES tf" +
                " WHERE tf.ESTADO = '1'";
        }
    }
}
