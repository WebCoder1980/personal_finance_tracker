using Microsoft.AspNetCore.Diagnostics;
using PersonalFinanceTracker.Users.Application.Exceptions;
using PersonalFinanceTracker.Users.Domain.Exceptions;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;
using System.Net;

namespace PersonalFinanceTracker.Users.Infrastructure.Middleware
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
                InvalidCredentialsException ex => (
                    (int)HttpStatusCode.Unauthorized,
                    ex.Message),
                UserNameAlreadyExistsException ex => (
                    (int)HttpStatusCode.Conflict,
                    ex.Message),
                InvalidDataException ex => (
                    (int)HttpStatusCode.BadRequest,
                    ex.Message),
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