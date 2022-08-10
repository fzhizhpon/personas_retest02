using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.MensajesRespuesta
{
    public class MensajesRespuestaResponse
    {
        public string id { get; set; }
        public Errors errors { get; set; }

        public class Errors
        {
            public string id { get; set; }
            public Error error { get; set; }

        }

        public class Error
        {
            public string id { get; set; }
        }

    }
}
