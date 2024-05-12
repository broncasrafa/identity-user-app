using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Api.Filters;

public class ValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                                    .Where(c => c.Errors.Count > 0)
                                    .SelectMany(c => c.Errors)
                                    .Select(c => c.ErrorMessage)
                                    .ToList();
            
            context.Result = new BadRequestObjectResult(new
            {
                Succeeded = false,
                Message = "Ocorreram uma ou mais falhas de validação.",
                Errors = errors
            });
        }
    }
}
