namespace NZWalks.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> logger;
        private readonly RequestDelegate next;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            logger.LogInformation($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");
            await next(httpContext);
        }
    }
}
