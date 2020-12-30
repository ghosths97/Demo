using Demo.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Filters
{
    public class DemoExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is DemoException demoException)
            {
                context.HttpContext.Response.StatusCode = (int)demoException.statusCode;
                context.Result = new JsonResult(new { message = demoException.Message });
                
                return;
            }

            base.OnException(context);
        }

    }
}
