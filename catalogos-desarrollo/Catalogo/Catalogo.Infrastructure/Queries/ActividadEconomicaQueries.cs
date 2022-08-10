using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Queries
{
    public static class ActividadEconomicaQueries
    {

        public static string SelectActividadesEconomicas(string esquema)
        {
            return "SELECT" +
                   "   p.CODIGO codigo," +
                   "   p.DESCRIPCION descripcion, " +
                   "   p.NIVEL nivel, " +
                   "   p.ECONOMICA economica " +
                   $" FROM {esquema}.SIFV_ACTIVIDADES_ECONOMICAS p ";
        }
    }
}
