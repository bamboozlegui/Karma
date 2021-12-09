using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Karma.Middleware
{
    public class RequestLoggingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;

            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
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
                _logger.LogInformation(
                    "USER {name} POSTED IN {url} => STATUSCODE {statusCode}",
                    context.User.Identity.Name,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode
                    );
            }
        }
    }
}
