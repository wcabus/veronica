using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Veronica.Backend.WebApi.Filters
{
    public class ValidationAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var validationException = context.Exception as ValidationException;
            if (validationException == null)
            {
                return;
            }
            
            // Transform the exception into a 400 Bad Request response
            var modelState = new ModelStateDictionary();
            foreach (var error in validationException.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            context.Result = new BadRequestObjectResult(modelState);
            context.ExceptionHandled = true;
        }
    }
}