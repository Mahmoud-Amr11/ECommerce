using DomainLayer.Exceptions;
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

                //Response
                if(httpContext.Response.StatusCode== StatusCodes.Status404NotFound)
                {
                    _logger.LogWarning("Resource not found: {Path}", httpContext.Request.Path);
                    var notFound = new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"The resource {httpContext.Request.Path} you are looking for does not exist"
                    };

                    await httpContext.Response.WriteAsJsonAsync(notFound);
                }
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Something went wrong");

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError

                };


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
