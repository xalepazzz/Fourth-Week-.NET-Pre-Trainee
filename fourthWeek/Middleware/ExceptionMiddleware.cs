using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace fourthWeek.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = exception.Message.ToLower();

            if (errorMessage.Contains("не найден") ||
                errorMessage.Contains("не существует") ||
                errorMessage.Contains("id") && errorMessage.Contains("не найден"))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { error = "Не найдено", details = exception.Message });
            }
            else if (errorMessage.Contains("не может быть") ||
                     errorMessage.Contains("должен быть") ||
                     errorMessage.Contains("должна быть") ||
                     errorMessage.Contains("некорректные") ||
                     exception is ArgumentException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Ошибка валидации", details = exception.Message });
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Ошибка сервера" });
                Console.WriteLine(exception.Message);
            }
        }
    }

    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
