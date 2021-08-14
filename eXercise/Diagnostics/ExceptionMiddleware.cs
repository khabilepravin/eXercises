using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace eXercise.Diagnostics
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>(); 
        }
    
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate requestDelegate)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                string errorVerbose = $"{ex.Message}{Environment.NewLine}{ex.StackTrace}";
                _logger.LogError(errorVerbose);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {   
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(exception.Message.ToString());
        }
    }
}
