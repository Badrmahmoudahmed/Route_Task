using Route_Task.ErrorHandler;
using System.Net;
using System.Text.Json;

namespace Route_Task.Middllewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _loggerFactory;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> loggerFactory, IWebHostEnvironment env)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError(ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var Response = _env.IsDevelopment() ? new ExptionError(httpContext.Response.StatusCode, ex.Message, ex.StackTrace)
                    :
                    new ExptionError(httpContext.Response.StatusCode);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(Response, options));
            }
        }
    }
}
