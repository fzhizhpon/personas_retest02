using Catalogo.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Catalogo.Core.Interfaces.DataBase;
using Oracle.ManagedDataAccess.Client;

namespace Catalogo.Infrastructure.Data
{
    public class ConexionOracle : IConexion<OracleConnection>
    {
        private readonly DbConexionOption _parametros;

        public ConexionOracle(IOptions<DbConexionOption> parametros)
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
