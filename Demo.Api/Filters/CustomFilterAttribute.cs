using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Filters
{
    public class CustomFilterAttribute : ActionFilterAttribute, IActionFilter
    {

        public string scope { get; set; }
        public CustomFilterAttribute(string scope)
        {
            //_logger = logger;
            this.scope = scope;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
           // var logger  = context.HttpContext.RequestServices.GetService(typeof(ILogger));
            //logger.LogInformation("On Action Executed");
            Console.WriteLine($"On Action Executed from {scope}");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"On Action Executing from {scope}");
            // _logger.LogInformation("On Action Executing");
        }
    }
}
