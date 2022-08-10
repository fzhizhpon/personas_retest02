namespace Personas.Core.Dtos.Personas
{
    public class GuardarPersonaDto
    {
        public string numeroIdentificacion { get; set; }

        public string observaciones { get; set; }

        public int codigoTipoIdentificacion { get; set; }

        public int codigoTipoPersona { get; set; }

        public int codigoTipoContribuyente { get; set; }

        public int codigoAgencia { get; set; }
    }
}
/*
 	"codigoPersona" : 12342134,
	"razonSocial" : "razonSocial",
	"fechaConstitucion" : "2022-02-04",
	"objetoSocial" : "",
	"tipoSociedad" : 1,
	"finalidadLucro" : "",
	"obligadoLlevarContabilidad" : "",
	"agenteRetencion" : "",
	"direccionWeb" : "",

	"ingresoPromedio" : 1000,
	"observaciones" : "",
	"codigoTipoIdentificacion" : 1,
	"codigoTipoPersona" : 1,
	"razonSocial" : "Mi Banquito SA.",
	"finalidadLucro" : ""
 
 */