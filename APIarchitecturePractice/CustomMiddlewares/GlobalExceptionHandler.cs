using APIarchitecturePractice.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace APIarchitecturePractice.CustomMiddlewares
{
    public class GlobalExceptionHandler
    {

        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }


        private async Task HandleException(HttpContext context, Exception exception)
        {
           
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            GlobalErrorResponse errorResponse = new GlobalErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Message from GlobalExceptionHandler : {exception.Message}"
            };

            await context.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
