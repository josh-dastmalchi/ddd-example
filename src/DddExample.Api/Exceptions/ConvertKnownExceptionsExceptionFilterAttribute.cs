using System.Threading.Tasks;
using DddExample.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DddExample.Api.Exceptions
{
    // Mappings to convert known exception types into HTTP status codes
    public sealed class ConvertKnownExceptionsExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            var message = context.Exception.Message ?? string.Empty;

            if (typeof(ValidationException).IsAssignableFrom(type))
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(message);
            }
            else if (typeof(EntityNotFoundException).IsAssignableFrom(type))
            {
                context.ExceptionHandled = true;
                context.Result = new NotFoundObjectResult(message);
            }

            return base.OnExceptionAsync(context);
        }
    }
}