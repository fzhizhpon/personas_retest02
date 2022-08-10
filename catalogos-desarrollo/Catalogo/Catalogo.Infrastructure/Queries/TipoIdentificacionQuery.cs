namespace Catalogo.Infrastructure.Queries
{
    public static class TipoIdentificacionQuery
    {

        public static string SelectTiposIdentificaciones(string esquema)
        {
            return "SELECT" +
                " sti.CODIGO_TIPO_IDENTIFICACION codigo," +
                " sti.DESCRIPCION," +
                " sti.OBSERVACIONES" +
                $" FROM {esquema}.SIFV_TIPOS_IDENTIFICACIONES sti" +
                " WHERE sti.ESTADO = '1'";
        }
    }
}
