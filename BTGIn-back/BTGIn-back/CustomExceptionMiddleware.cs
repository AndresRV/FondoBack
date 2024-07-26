using System.Net;
using BTGIn_back.Business.Exceptions;
using System.Text.Json;

namespace BTGIn_back
{
    public class CustomExceptionMiddleware
    {
        private readonly static Dictionary<Type, HttpStatusCode> ExceptionStatusCodes = new()
        {
            { typeof(FundAlreadyRegistredException), HttpStatusCode.Conflict },
            { typeof(InsufficientCashException), HttpStatusCode.BadRequest },
            { typeof(KeyNotFoundException), HttpStatusCode.NotFound }
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if (!ExceptionStatusCodes.TryGetValue(exception.GetType(), out HttpStatusCode codigoHttp))
                codigoHttp = HttpStatusCode.InternalServerError;

            context.Response.StatusCode = (int)codigoHttp;

            var response = new { message = exception.Message };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
