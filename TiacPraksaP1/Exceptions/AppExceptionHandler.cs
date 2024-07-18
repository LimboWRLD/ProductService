
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Products.Exceptions.CustomExceptions;

namespace Products.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler>logger) 
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                ForbidException => (403, null),
                BadHttpRequestException badRequestException => (400, badRequestException.Message),
                NotFoundException notFoundException => (404, notFoundException.Message),
                UnauthorizedException unauthorizedAccessException => (401, unauthorizedAccessException.Message),
                _ => default
            };
            if (statusCode == default)
            {
                return false;
            }
            _logger.LogError(statusCode, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMessage); 

            return true;
        }
    }
}
