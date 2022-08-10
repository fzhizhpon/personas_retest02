using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Personas.Core.Dtos.ReferenciasComerciales
{
    public class ObtenerReferenciaComercialDto
    {
        public int? codigoPersona { get; set; }
        public int? numeroRegistro { get; set; }

        [JsonIgnore]
        public char estado { get; } = '1';

    }
}
