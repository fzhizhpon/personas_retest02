namespace Personas.Core.Dtos.Representantes
{
    public class ObtenerRepresentanteDto
    {
        public int codigoPersona { get; set; }
        public int codigoRepresentante { get; set; }
        public char estado { get; set; } = '1';

    }
}
