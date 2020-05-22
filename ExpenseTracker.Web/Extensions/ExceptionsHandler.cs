using ExpenseTracker.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseTracker.Web.Extensions
{
    public class ExceptionsHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            Logger.Log<ExceptionsHandler>(exception);

            context.ExceptionHandled = true; 
        }
    }
}
