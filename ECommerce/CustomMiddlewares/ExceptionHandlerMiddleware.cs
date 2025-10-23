using Shared.ErrorModels;

namespace ECommerce.Web.CustomMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Something went wrong");

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


                var errorToReturn =new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = _environment.IsDevelopment()
                                ? ex.Message
                                : "An internal server error occurred"
                };


                 await httpContext.Response.WriteAsJsonAsync(errorToReturn);
            }
        }
    }
}
