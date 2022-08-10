namespace Catalogo.Infrastructure.Queries
{
    public class UsuariosDatabaseQuery
    {
        public static string ObtenerUsuariosDatabase(string esquema)
        {
            return "SELECT " +
                "SUBD.CODIGO_USUARIO codigo, " +
                "SUBD.USUARIO descripcion " +
                $"FROM {esquema}.SEG_USUARIOS_BASE_DATOS SUBD " +
                " WHERE SUBD.CODIGO_ESTADO = '1' ";
        }
    }
}