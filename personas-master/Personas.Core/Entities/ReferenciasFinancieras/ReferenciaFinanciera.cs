using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Personas.Core.Entities.ReferenciasFinancieras
{
    public class ReferenciaFinanciera
    {


        public int codigoTipoInstitucionFinanciera { get; set; }
        public long numeroRegistro { get; set; }
        public int codigoTipoCuentaFinanciera { get; set; }
        public string numeroCuenta { get; set; }
        public long codigoPersona { get; set; }
        public string cifras { get; set; }
        public DateTime fechaCuenta { get; set; }
        public string observaciones { get; set; }
        public float saldo { get; set; }
        public int codigoInstitucionFinanciera { get; set; }
        public string nombreFinanciera { get; set; }
        public float saldoObligacion { get; set; }
        public float obligacionMensual { get; set; }


        [JsonIgnore]
        public int codigoUsuarioRegistra { get; set; }
        [JsonIgnore]
        public DateTime fechaUsuarioRegistra { get; set; }
        [JsonIgnore]
        public string guid { get; set; }

        //public int codigoReferenciaFinanciera { get; set; }
        //public int codigoUsuarioRegistro { get; set; }
        //public DateTime fechaUsuarioRegistro { get; set; }
        //public string guid { get; set; }

        //public class ReferenciaFinancieraMinimo
        //{
        //    public int codigoReferenciaFinanciera { get; set; }
        //    public int codigoPersona { get; set; }
        //    public int codigoInstitucionFinanciera { get; set; }
        //    public string numeroCuenta { get; set; }
        //}

    }
}
