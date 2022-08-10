namespace Catalogo.Infrastructure.Queries
{
    public static class ProfesionQuery
    {
        public static string SelectProfesiones(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PROFESION codigo," +
                "   p.NOMBRE descripcion" +
                $" FROM {esquema}.SIFV_PROFESIONES p" +
                $" ORDER BY p.NOMBRE";
        }

        public static string SelectProfesion(string esquema)
        {
            return "SELECT" +
                "   p.CODIGO_PROFESION codigo," +
                "   p.NOMBRE," +
                "   p.OBSERVACION," +
                "   p.ESTADO," +
                "   p.CODIGO_ALTERNO codigoAlterno" +
                $" FROM {esquema}.SIFV_PROFESIONES p" +
                " WHERE p.CODIGO_PROFESION = :codigoProfesion";
        }

        public static string InsertProfesion(string esquema)
        {
            return $"INSERT INTO {esquema}.SIFV_PROFESIONES(" +
                "   NOMBRE," +
                "   OBSERVACION," +
                "   ESTADO," +
                "   CODIGO_ALTERNO) " +
                " VALUES(" +
                "   :nombre," +
                "   :observacion," +
                "   :estado," +
                "   :codigoAlterno)";
        }

        public static string DeleteProfesion(string esquema)
        {
            return $"DELETE FROM {esquema}.SIFV_PROFESIONES sp " +
                " WHERE sp.CODIGO_PROFESION = :codigoProfesion";
        }

        public static string UpdateProfesion(string esquema)
        {
            return $"UPDATE {esquema}.SIFV_PROFESIONES SET " +
                "   NOMBRE = :nombre, " +
                "   OBSERVACION = :observacion, " +
                "   ESTADO = :estado, " +
                "   CODIGO_ALTERNO = :codigoAlterno" +
                " WHERE CODIGO_PROFESION = :codigo";
        }
    }
}
