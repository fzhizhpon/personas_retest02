using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.BienesIntangibles
{
    public class GuardarBienesIntangiblesDto : AuditDto
    {
        public long codigoPersona { get; set; }
        [JsonIgnore] public long numeroRegistro { get; set; }
        public int tipoBienIntangible { get; set; }
        public string codigoReferencia { get; set; }
        public string descripcion { get; set; }
        public float avaluoComercial { get; set; }

        [JsonIgnore]
        public char estado { get; set; }
    }
}