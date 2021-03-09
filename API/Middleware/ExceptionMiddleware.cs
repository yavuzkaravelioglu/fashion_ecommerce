using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env) // inject few thing into it
        {
            this._env = env;
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // it means; if there is no exception then the request moves on to its next stage.
            }
            catch(Exception ex) // if there is an exception; this is where we can use our own exception handling response.
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json"; // write our own response into the context response, 
                                                                   // so that we can send it to the client.
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Basically means that we're going to set the 
                                                                                       // status code to be a 500 Internal Server Error.

                var response = _env.IsDevelopment()
                    ? new ApiException( (int)HttpStatusCode.InternalServerError, ex.Message, // if we are in a Development Mode
                        ex.StackTrace.ToString() )
                    : new ApiException( (int)HttpStatusCode.InternalServerError ); // else if we are in Production Mode 

                var options = new JsonSerializerOptions{PropertyNamingPolicy =
                    JsonNamingPolicy.CamelCase}; // Döndurulen Error Json ınındaki Baş Harfler İçin
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        } 

    }
}