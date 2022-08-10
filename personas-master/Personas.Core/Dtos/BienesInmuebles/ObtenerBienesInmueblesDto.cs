using Vimasistem.QueryFilter.Attributes;

namespace Personas.Core.Dtos.BienesInmuebles
{
    public class ObtenerBienesInmueblesDto : PaginacionQueryDto
    {
        [Filter("CODIGO_PERSONA", "PERS_BIENES_INMUEBLES")]
        public int? codigoPersona { get; set; }

        [Filter("NUMERO_REGISTRO", "PERS_BIENES_INMUEBLES")]
        public int? numeroRegistro { get; set; }

        [Filter("SECTOR", "PERS_BIENES_INMUEBLES")]
        public string sector { get; set; }

        [Filter("CALLE_PRINCIPAL", "PERS_BIENES_INMUEBLES")]
        public string callePrincipal { get; set; }

        [Filter("CALLE_SECUNDARIA", "PERS_BIENES_INMUEBLES")]
        public string calleSecundaria { get; set; }

        [Filter("TIPO_BIEN_INMUEBLE", "PERS_BIENES_INMUEBLES")]
        public int tipoBienInmueble { get; set; }

        [Filter("ESTADO", "PERS_BIENES_INMUEBLES")]
        public char estado { get; } = '1';
    }
}
