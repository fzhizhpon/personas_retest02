using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.TablasComunes
{
    public class GuardarInformacionAdicionalDto
    {
        public long codigoReferencia { get; set; }
        public long codigoTabla { get; set; }
        public long codigoElemento { get; set; }
        public string observacion { get; set; }

        public int codigoModulo { get; set; }
        [JsonIgnore] public char estado { get; set; }
        [JsonIgnore] public int codigoUsuarioActualiza { get; set; }
        [JsonIgnore] public DateTime fechaUsuarioActualiza { get; set; }
    }
}