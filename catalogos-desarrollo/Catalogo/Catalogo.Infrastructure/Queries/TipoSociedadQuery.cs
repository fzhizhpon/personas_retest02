namespace Catalogo.Infrastructure.Queries
{
    public static class TipoSociedadQuery
    {
        public static string SelectTiposSociedades(string esquema)
        {
            return $@"SELECT
	                    tse.CODIGO,
	                    tse.DESCRIPCION
                    FROM {esquema}.SIFV_TIPOS_SOCIEDADES_EMPRESAS tse
                    ORDER BY ORDEN";
        }
    }
}
