using Vimasistem.QueryFilter.Attributes;

namespace Personas.Core.Dtos.Personas
{
    public class PersonaRequest : PaginacionQueryDto
    {
        [Filter("CODIGO_PERSONA", "PERS_PERSONAS")]
        public int? codigoPersona { get; set; }

        [Filter("nombre", "pers")]
        public string nombre { get; set; }

        [Filter("NUMERO_IDENTIFICACION", "PERS_PERSONAS")]
        public string nroIdentificacion { get; set; }
    }
}

