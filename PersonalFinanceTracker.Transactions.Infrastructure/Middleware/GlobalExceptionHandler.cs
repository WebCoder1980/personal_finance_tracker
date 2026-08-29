using Microsoft.AspNetCore.Diagnostics;
using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Infrastructure.Dtos;
using System.Net;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            context.Response.ContentType = "application/json";

            (int statusCode, string message) = exception switch
            {
                NotFoundException ex => (
                    (int)HttpStatusCode.NotFound,
                    "Not found error"),
                DomainException ex => (
                    (int)HttpStatusCode.BadRequest,
                    "Invalid data error"),
                _ => (
                    (int)HttpStatusCode.InternalServerError,
                    "Internal server error")
            };

            context.Response.StatusCode = statusCode;

            var response = new ErrorDto(message);

            _logger.LogWarning(exception.ToString());

            await context.Response.WriteAsJsonAsync(
                response,
                cancellationToken);

            return true;
        }
    }
}