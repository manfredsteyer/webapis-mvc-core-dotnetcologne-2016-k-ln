using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlugDemo.Components
{

    public class LogFilter : Attribute, IActionFilter, IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.HttpContext.TraceIdentifier;
            var actionName = context.ActionDescriptor.Name;
            var parameters = new StringBuilder();

            foreach(var p in context.ActionArguments) {
                parameters.Append(p.Key + "=" + p.Value + ", ");
            }
            if (parameters.Length > 0) parameters.Length -= 2;

            Debug.WriteLine($"OnActionExecuting: {id} {actionName}({parameters.ToString()})");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var id = context.HttpContext.TraceIdentifier;
            // Use your Logger here ...
            Debug.WriteLine("OnActionExecuted: " + id);
        }

        public void OnException(ExceptionContext context)
        {
            var id = context.HttpContext.TraceIdentifier;
            // Use your Logger here ...
            Debug.WriteLine("OnException: " + id);

        }
    }
}
