using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Exceptions.WishlistExceptions;
using System.Text.Json;

namespace Project_Keystone.Api.Middleware
{
    
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                UserNotFoundException or KeyNotFoundException or ProductNotFoundException or WishlistNotFoundException or WishlistItemNotFoundException => StatusCodes.Status404NotFound,
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                UserRegistrationFailedException or UserUpdateFailedException or PasswordChangeFailedException
                    or ProductCreationFailedException or ProductUpdateFailedException or ProductDeletionFailedException
                    or WishlistItemAlreadyExistsException or WishlistOperationFailedException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };


            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
