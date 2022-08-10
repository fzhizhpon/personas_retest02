using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.EstadosFinancieros
{
    public class GuardarEstadoFinancieroDto
    {
        public int codigoPersona { get; set; }
        public string cuentaContable { get; set; }
        public float valor { get; set; }
        public string observacion { get; set; }
        [JsonIgnore] public DateTime fechaUsuarioActualiza { get; set; }
        [JsonIgnore] public int codigoUsuarioActualiza { get; set; }


    }
}
