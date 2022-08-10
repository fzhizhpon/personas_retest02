using Catalogo.Core.Enums;

namespace Catalogo.Core.DTOs
{
    public class Respuesta
    {
        public ERespuesta codigo { get; set; }

        public object resultado { get; set; }

        public string mensajeUsuario { get; set; }
    }
}
