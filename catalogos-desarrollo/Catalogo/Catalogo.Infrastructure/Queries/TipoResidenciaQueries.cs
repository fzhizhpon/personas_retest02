using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Queries
{
    public static class TipoResidenciaQueries
    {
        public const string TIPOS_RESIDENCIAS = "SIFV_TIPOS_RESIDENCIAS";

        public static string ObtenerTiposResidencias(string esquema)
        {
            return " SELECT str.CODIGO as codigo," +
                   " str.DESCRIPCION as descripcion" +
                   $" FROM {esquema}.{TIPOS_RESIDENCIAS} str" +
                   " WHERE str.ESTADO = '1'" +
                   " ORDER BY str.ORDEN ";
        }
    }
}
