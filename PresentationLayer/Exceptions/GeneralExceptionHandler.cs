﻿using Microsoft.AspNetCore.Diagnostics;

namespace Products.Exceptions
{
    public class GeneralExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> _logger;
        public GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Something went wrong..");

            httpContext.Response.StatusCode = 501;
            await httpContext.Response.WriteAsJsonAsync("Something went wrong..", cancellationToken);    
            return true;
        }
    }
}
