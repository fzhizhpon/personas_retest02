using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personas.Infrastructure.Validadores;
using ValidationResult = FluentValidation.Results.ValidationResult;
using Personas.Core.App;

namespace Personas.Infrastructure.Configuraciones
{
    public class ValidadorInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                actionContext.HttpContext.Items.Add("ValidationResult", result);
            }
            return result;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

    }

    public class ValidationResultAttribute : ActionFilterAttribute
    {
        //OnActionExecuting => ActionExecutingContext

        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            if (!ctx.HttpContext.Items.TryGetValue("ValidationResult", out var value))
            {
                return;
            }
            if (!(value is ValidationResult vldResult))
            {
                return;
            }

            var model = vldResult.Errors.Select(err => new ModeloValidador(err)).ToArray();

            Respuesta respuesta = new Respuesta()
            {
                codigo = -1,
                mensaje = String.Join(",", model.Select(m => $"{m.campo}: {m.errorMsg}")),
                resultado = model
            };

            ctx.Result = new BadRequestObjectResult(respuesta);
        }

    }
}
