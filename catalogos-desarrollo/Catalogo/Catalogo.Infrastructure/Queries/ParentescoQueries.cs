using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Queries
{
    public class ParentescoQueries
    {
        public const string PARENTESCOS = "SIFV_PARENTESCOS";

        public static string ObtenerParentescos(string esquema)
        {
            return " SELECT sp.CODIGO_PARENTESCO as codigoParentesco," +
                   " sp.NOMBRE as nombre," +
                   " sp.GRADO_CONSANGUINIDAD as gradoConsanguinidad," +
                   " sp.GRADO_AFINIDAD as gradoAfinidad" +
                   $" FROM {esquema}.{PARENTESCOS} sp" +
                   " WHERE sp.ESTADO = '1'" +
                   " ORDER BY sp.ORDEN ";
        }
    }
}
