using Vimasistem.QueryFilter.Attributes;

namespace Personas.Core.Dtos.Representantes;

public class RepresentanteRequest : PaginacionQueryDto
{
    [Filter("CODIGO_PERSONA", "rep")] public int? codigoPersona { get; set; }

    [Filter("CODIGO_TIPO_REPRESENTANTE", "rep")]
    public int? codigoTipoRepresentante { get; set; }    
    
    [Filter("ESTADO", "rep")]
    public char? estado { get; set; }
}