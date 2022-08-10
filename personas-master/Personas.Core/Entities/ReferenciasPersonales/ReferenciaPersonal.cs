using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.ReferenciasPersonales
{
    public class ReferenciaPersonal
    {

        public int numeroRegistro { get; set; }
        public int codigoPersonaReferida { get; set; }
        public int codigoPersona { get; set; }
        public DateTime fechaConoce { get; set; }
        public string observaciones { get; set; }
        public int codigoUsuarioRegistro { get; set; }
        public DateTime fechaUsuarioRegistro { get; set; }
        public string guid { get; set; }


        public class ReferenciaPersonalMinimo
        {
            public int numeroRegistro { get; set; }
            public int codigoPersonaReferida { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public int codigoPersona { get; set; }
            public DateTime fechaConoce { get; set; }
        }

        public class ReferenciaPersonalJoin
        {
            public int numeroRegistro { get; set; }
            public string identificacion { get; set; }
            public int codigoTipoIdentificacion { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public int codigoPersonaReferida { get; set; }
            public int codigoPersona { get; set; }
            public DateTime fechaConoce { get; set; }
            public string observaciones { get; set; }
        }

    }
}
