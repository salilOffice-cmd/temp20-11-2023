using APIarchitecturePractice.Controllers;
using APIarchitecturePractice.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIarchitecturePractice.CustomMiddlewares
{
    public class thirdCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public thirdCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if(context.Request.Path == "/api/Customer/getCustomerAndOrders")
            {
                // Retrieve the controller from the service provider
                //var customerController = serviceProvider.GetService<CustomerController>();
                // Pass the HttpContext to the controller
                //customerController.SetHttpContext(context);

                Console.WriteLine($"Request3 Method3: {context.Request.Method}, Route3: {context.Request.Path}");
                
            }

            try
            {
                //throw new Exception("Exception from third middleware");
                await _next(context);
                //await context.Response.WriteAsJsonAsync(new { message = "Response from middleware" });

            }

            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
