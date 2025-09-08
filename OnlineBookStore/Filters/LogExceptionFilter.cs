using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace OnlineBookStore.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Debug.WriteLine($"Error: {context.Exception.Message}");
            context.ExceptionHandled = true;
        }
    }
}
