using Catalogo.Infrastructure.Validadores;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Catalogo.Infrastructure.Configuraciones
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
        public override void OnActionExecuting(ActionExecutingContext ctx)
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
            ctx.Result = new BadRequestObjectResult(model);
        }

    }
}
