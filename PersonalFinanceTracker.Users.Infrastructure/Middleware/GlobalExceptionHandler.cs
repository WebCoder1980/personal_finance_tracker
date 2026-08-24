using Microsoft.AspNetCore.Diagnostics;
using PersonalFinanceTracker.Users.Application.Exceptions;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;
using System.Net;

namespace PersonalFinanceTracker.Users.Infrastructure.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
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
                _ => (
                        (int)HttpStatusCode.InternalServerError,
                        "Internal server error")
                };

            context.Response.StatusCode = statusCode;

            var response = new ErrorDto(message);

            Console.WriteLine(exception);

            await context.Response.WriteAsJsonAsync(
                response,
                cancellationToken);

            return true;
        }
    }
}