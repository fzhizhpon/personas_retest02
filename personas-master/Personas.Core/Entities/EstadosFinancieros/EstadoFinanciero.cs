using System.Text.Json.Serialization;

namespace Personas.Core.Entities.EstadosFinancieros
{
    public class EstadoFinanciero
    {
        public string cuentaContable { get; set; }
        public string descripcion { get; set; }
        public double? valor { get; set; }
        public string observacion { get; set; }

        [JsonIgnore]
        public string? query { get; set; }
        public int? codigoComponente { get; set; }

        public bool recursoExterno { get; set; }
    }
}
