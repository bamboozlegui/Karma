using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Karma.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;

            _logger = loggerFactory.CreateLogger<ErrorLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError("Unexpected error -> {error}", e);
                throw;
            }
            finally
            {
                if(context.Response?.StatusCode > 399)
                {
                    _logger.LogError(
                        "Error in request {method} {url} => {statusCode}",
                        context.Request?.Method,
                        context.Request?.Path.Value,
                        context.Response?.StatusCode
                        );


                }
                
            }
        }

    }
}
