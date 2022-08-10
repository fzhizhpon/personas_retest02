namespace Catalogo.Infrastructure.Queries
{
    public static class NivelInstruccionQuery
    {
        public static string SelectNivelesIntruccion(string esquema)
        {
            return "SELECT" +
                "   ni.CODIGO_NIVEL_INSTRUCCION codigo," +
                "   ni.NOMBRE descripcion" +
                $" FROM {esquema}.SIFV_NIVELES_INSTRUCCION ni";
        }
    }
}
