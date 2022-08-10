using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Queries
{
    public static class LugaresQueries
    {
        public const string VISTA_LUGARES = "VISTA_LUGARES";
        public static string ObtenerLugares(string esquema)
        {
            return " SELECT CODIGO_PAIS as codigoPais," +
                   " PAIS as pais," +
                   " NACIONALIDAD as nacionalidad," +
                   " CODIGO_PROVINCIA as codigoProvincia," +
                   " PROVINCIA as provincia," +
                   " CODIGO_CIUDAD as codigoCiudad," +
                   " CIUDAD as ciudad," +
                   " CODIGO_PARROQUIA as codigoParroquia," +
                   " PARROQUIA as parroquia," +
                   " CODIGO_LUGAR as codigoLugar," +
                   " DESCRIPCION_LUGAR as descripcionLugar" +
                   $" FROM {esquema}.{VISTA_LUGARES} ";
        }
    }
}
