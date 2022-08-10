using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.Representantes
{
    public class Representante
    {
        public long codigoPersona { get; set; }
        public long codigoRepresentante { get; set; }
        public int codigoTipoRepresentante { get; set; }
        public char principal { get; set; }
        public char estado { get; set; }
        public int codigoUsuarioRegistra { get; set; }
        public DateTime fechaUsuarioRegistra { get; set; }

        public class RepresentanteMinimo
        {
            public long codigoPersona { get; set; }
            public long codigoRepresentante { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public int codigoTipoRepresentante { get; set; }
            public char principal { get; set; }
            public char estado { get; } = '1';
        }

        public class RepresentanteJoinFull
        {
            public long codigoPersona { get; set; }
            public long codigoRepresentante { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public int codigoTipoRepresentante { get; set; }
            public char principal { get; set; }
        }

        public class RepresentanteJoin
        {
            public long codigoPersona { get; set; }
            public long codigoRepresentante { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public string numeroIdentificacion { get; set; }
            public int codigoTipoRepresentante { get; set; }
            public char principal { get; set; }
        }

        public class RepresentanteSimple
        {
            public long codigoPersona { get; set; }
            public long codigoRepresentante { get; set; }
            public char principal { get; set; }
            public char estado { get; } = '1';
        }

    }
}
