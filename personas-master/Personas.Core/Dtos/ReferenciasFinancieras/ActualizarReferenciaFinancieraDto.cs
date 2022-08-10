using System;
using System.Text.Json.Serialization;

namespace Personas.Core.Dtos.ReferenciasFinancieras
{
    public class ActualizarReferenciaFinancieraDto
    {
        public long numeroRegistro { get; set; }
        public int codigoPersona { get; set; }
        public string cifras { get; set; }
        public double saldo { get; set; }
        public float saldoObligacion { get; set; }
        public float obligacionMensual { get; set; }
        public string observaciones { get; set; }
        //public string numeroCuenta { get; set; }
        //public int codigoInstitucionFinanciera { get; set; }

        //public DateTime? fechaCuenta { get; set; }


        [JsonIgnore]
        public string guid { get; set; }
        [JsonIgnore]
        public DateTime fechaUsuarioActualiza { get; set; }
        [JsonIgnore]
        public long codigoUsuarioActualiza { get; set; }
    }
}
