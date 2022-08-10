using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Queries
{
    public static class TipoRepresentanteQueries
    {
        public const string TIPOS_REPRESENTANTES = "SIFV_TIPOS_REPRESENTANTES";

        public static string ObtenerTiposRepresentantes(string esquema)
        {
            return " SELECT str.CODIGO as codigo," +
                   " str.DESCRIPCION as descripcion" +
                   $" FROM {esquema}.{TIPOS_REPRESENTANTES} str" +
                   " WHERE str.ESTADO = '1'" +
                   " ORDER BY str.ORDEN ";
        }
    }
}
