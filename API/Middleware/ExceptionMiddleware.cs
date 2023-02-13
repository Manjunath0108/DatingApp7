using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware>logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context){
            try{
                await _next(context);
            }
            catch(Exception ex){
                    _logger.LogError(ex,ex.Message);
                    context.Response.ContentType="application/json";
                    context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                    var response=_env.IsDevelopment()
                        ? new ApiException(context.Response.StatusCode,ex.Message, ex.StackTrace?.ToString())
                        : new ApiException(context.Response.StatusCode,ex.Message,"Internal Server Error");

                        var options = new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

                        var json=JsonSerializer.Serialize(response,options);

                        await context.Response.WriteAsync(json);
            }
        }
        
    }
}



/*

 The purpose of the middleware is to handle exceptions that occur during the processing of a 
 request in the pipeline. The middleware takes in a RequestDelegate (next middleware in the pipeline),
  ILogger for logging purposes, and IHostEnvironment to determine whether the application is running in a 
  development environment. The middleware logs the exception and sets the response content type to "application/json".
   The response status code is set to HttpStatusCode.InternalServerError and an ApiException object is serialized to 
   JSON and returned as the response body. The ApiException object includes the response status code and an error 
   message. In a development environment, the stack trace of the exception is also included in the response body.
*/