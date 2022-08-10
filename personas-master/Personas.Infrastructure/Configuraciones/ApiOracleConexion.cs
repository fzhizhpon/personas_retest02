using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Core.Interfaces.DataBase;
using Personas.Core.App;

namespace Personas.Infrastructure.Configuraciones
{
    public class ApiOracleConexion : IConexion<OracleConnection>
    {
        private readonly ConexionDb _parametros;

        public ApiOracleConexion(IOptions<ConexionDb> parametros)
        {
            _parametros = parametros.Value;
        }

        public OracleConnection ObtenerConexion()
        {
            string configuracion = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + _parametros.Host + ")(PORT=" + _parametros.Puerto
                + "))(CONNECT_DATA = (SERVICE_NAME = " + _parametros.NombreServicio
                + ")));USER ID = " + _parametros.Usuario + "; PASSWORD = " + _parametros.Clave + ";"; ;

            OracleConnection conexion = new OracleConnection(configuracion);

            return conexion;
        }
    }
}
