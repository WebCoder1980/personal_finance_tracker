using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Users.Dtos;
using Users.Exceptions;

namespace Users.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            context.Response.ContentType = "application/json";

            (int statusCode, string code, string message) = exception switch
            {
                InvalidCredentialsException ex => (
                    (int)HttpStatusCode.Unauthorized,
                    ex.Code,
                    ex.Message),
                UserAlreadyExistsException ex => (
                    (int)HttpStatusCode.Conflict,
                    ex.Code,
                    ex.Message),
                AuthException ex => (
                    (int)HttpStatusCode.BadRequest,
                    ex.Code,
                    ex.Message),
                InvalidDataException ex => (
                    (int)HttpStatusCode.BadRequest,
                    "invalid_data_exception",
                    ex.Message),
                _ => (
                        (int)HttpStatusCode.InternalServerError,
                        "internal_error",
                        "Internal server error")
                };

            context.Response.StatusCode = statusCode;

            var response = new ErrorDto(code, message);

            await context.Response.WriteAsJsonAsync(
                response,
                cancellationToken);

            return true;
        }
    }
}