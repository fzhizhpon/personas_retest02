namespace Catalogo.Infrastructure.Queries
{
    public class EstadoUsuariosQuery
    {
        public static string ObtenerEstadoUsuarios(string esquema)
        {
            return "SELECT " +
                "seu.CODIGO_ESTADO_USUARIO codigo, " +
                "seu.DESCRIPCION descripcion " +
                $"FROM {esquema}.SIFV_ESTADOS_USUARIOS seu";
        }
    }
}