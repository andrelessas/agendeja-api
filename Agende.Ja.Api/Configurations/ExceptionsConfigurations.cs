using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agende.Ja.Api.Configurations
{
    public class ExceptionsConfigurations : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(exception.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}
