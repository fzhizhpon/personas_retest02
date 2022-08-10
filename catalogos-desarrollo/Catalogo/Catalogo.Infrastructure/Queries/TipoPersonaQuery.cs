using Microsoft.Extensions.Configuration;

namespace Catalogo.Infrastructure.Queries
{
    public static class TipoPersonaQuery
    {

        public static string SelectTipoPersona(string esquema)
        {
            return "SELECT" +
                " stp.CODIGO_TIPO_PERSONA codigo," +
                " stp.DESCRIPCION" +
                $" FROM {esquema}.SIFV_TIPOS_PERSONAS stp" +
                " WHERE stp.ESTADO = '1'";
        }
    }
}
