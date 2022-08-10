using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Validadores
{
    public class ModeloValidador
    {
        public string campo { get; }
        public string errorMsg { get; }
        //public object AttemptedValue { get; }
        //public string ErrorCode { get; }

        public ModeloValidador(ValidationFailure error)
        {
            campo = error.PropertyName;
            errorMsg = error.ErrorMessage;
            //AttemptedValue = error.AttemptedValue;
            //ErrorCode = error.ErrorCode;
        }
    }
}
