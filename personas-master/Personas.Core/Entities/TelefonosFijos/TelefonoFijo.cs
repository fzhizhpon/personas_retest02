using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Entities.TelefonosFijos
{
    public class TelefonoFijo
    {
        public int codigoPersona { get; set; }
        public int codigoDireccion { get; set; }
        public string numero { get; set; }
        public int codigoOperadora { get; set; }
        public string observaciones { get; set; }
        public int numeroRegistro { get; set; }

        public class TelefonoFijoMinimo
        {
            public int numeroRegistro { get; set; }
            public int codigoPersona { get; set; }
            public string numero { get; set; }
            public int codigoOperadora { get; set; }
            public string operadora { get; set; }
            public int codigoDireccion { get; set; }

        }

        public class TelefonoFijoFull
        {

            public int codigoPersona { get; set; }
            public string numero { get; set; }
            public int codigoOperadora { get; set; }
            public string operadora { get; set; }
            public int codigoDireccion { get; set; }
            public int codigoParroquia { get; set; }
            public string sector { get; set; }
            public string callePrincipal { get; set; }
            public string calleSecundaria { get; set; }

        }

    }
}
