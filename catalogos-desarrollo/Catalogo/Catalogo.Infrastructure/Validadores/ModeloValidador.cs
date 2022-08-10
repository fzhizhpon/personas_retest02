using FluentValidation.Results;

namespace Catalogo.Infrastructure.Validadores
{
    public class ModeloValidador
    {
        public string campo { get; }
        
        public string errorMsg { get; }

        public ModeloValidador(ValidationFailure error)
        {
            campo = error.PropertyName;
            errorMsg = error.ErrorMessage;
        }
    }
}
