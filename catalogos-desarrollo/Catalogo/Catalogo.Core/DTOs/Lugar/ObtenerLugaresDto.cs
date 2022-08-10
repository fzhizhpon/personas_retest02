using Vimasistem.QueryFilter.Attributes;

namespace Catalogo.Core.DTOs.Lugar
{
    public class ObtenerLugaresDto : PaginacionDto
    {
        [Filter("CODIGO_PAIS")]
        public int? codigoPais { get; set; }

        [Filter("PAIS")]
        public string pais { get; set; }

        [Filter("NACIONALIDAD")]
        public string nacionalidad { get; set; }

        [Filter("CODIGO_PROVINCIA")]
        public int? codigoProvincia { get; set; }

        [Filter("PROVINCIA")]
        public string provincia { get; set; }

        [Filter("CODIGO_CIUDAD")]
        public int? codigoCiudad { get; set; }

        [Filter("CIUDAD")]
        public string ciudad { get; set; }

        [Filter("CODIGO_PARROQUIA")]
        public int? codigoParroquia { get; set; }

        [Filter("PARROQUIA")]
        public string parroquia { get; set; }

        [Filter("CODIGO_LUGAR")]
        public string codigoLugar { get; set; }

        [Filter("DESCRIPCION_LUGAR")]
        public string descripcionLugar { get; set; }
    }
}
