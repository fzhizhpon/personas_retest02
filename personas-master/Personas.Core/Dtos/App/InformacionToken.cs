namespace Personas.Core.Dtos.App
{
    public class InformacionToken
    {
        public int codigoUsuario { get; set; }
        public string usuario { get; set; }
        public int codigoAgencia { get; set; }
        public int codigoRol { get; set; }
        public int codigoPeriodo { get; set; }
        public string navegador { get; set; }
        public string ipPublica { get; set; }
        public string ipPrivada { get; set; }
    }
}
