using System;

namespace Personas.Core.Entities.Familiares
{
    public class Familiar
    {
        public int codigoPersonaFamiliar { get; set; }
        public int codigoPersona { get; set; }
        public int codigoParentesco { get; set; }
        public char estado { get; set; }
        public string observacion { get; set; }
        public int codigoUsuarioActualiza { get; set; }
        public DateTime fechaUsuarioActualiza { get; set; }
        public char esCargaFamiliar { get; set; }

        public class FamiliarJoinMinimo
        {

            public int codigoPersona { get; set; }
            public int codigoPersonaFamiliar { get; set; }
            public int codigoParentesco { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
        }

        public class FamiliarJoinFull
        {

            public int codigoPersona { get; set; }
            public int codigoPersonaFamiliar { get; set; }
            public int codigoParentesco { get; set; }
            public string observacion { get; set; }
            public char esCargaFamiliar { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }

        }
    }
}
