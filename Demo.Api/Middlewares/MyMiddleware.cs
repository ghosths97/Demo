using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware
    {
        public ILogger<MyMiddleware> _logger;

        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // todo before

            _logger.LogInformation("from my middleware");
            return _next(httpContext);

            // todo after
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
